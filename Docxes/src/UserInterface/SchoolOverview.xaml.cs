using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="SchoolOverview.xaml"/>.
    /// </summary>
    internal partial class SchoolOverview : Window {

        private BusinessLogic.BusinessObjectProcessor<Teacher, School> teacherProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> subjectProcessor = new BusinessLogic.SubjectProcessor();

        private BusinessLogic.BusinessObjectProcessor<Document, Subject> documentProcessor = new BusinessLogic.DocumentProcessor();
        private BusinessLogic.BusinessObjectProcessor<Note, Subject> noteProcessor = new BusinessLogic.NoteProcessor();
        private BusinessLogic.BusinessObjectProcessor<Grade, Subject> gradeProcessor = new BusinessLogic.GradeProcessor();
        private BusinessLogic.BusinessObjectProcessor<Event, Subject> eventProcessor = new BusinessLogic.EventProcessor();


        internal SchoolOverview() {
            InitializeComponent();

            Title = ApplicationPropertyManager.Workspace.School.Name;
            Common.ExtendWindowName(this);
        }

        #region Control

        private void UpdateWorkspace(Teacher teacher) {
            ApplicationPropertyManager.Workspace.Teacher = teacher;
        }

        private void UpdateWorkspace(Subject subject) {
            ApplicationPropertyManager.Workspace.Subject = subject;
        }

        #endregion

        #region Interface

        private bool CheckForTeacherCreation() {
            return (teacherProcessor.Get().Count == 0
                    && AskForElementCreation("Sie haben noch keine Lehrer eingetragen. Möchten Sie jetzt einen Lehrer hinzufügen?", "Lehrer hinzufügen?") == MessageBoxResult.Yes
                    && OpenAddTeacher() == BusinessObjectManagerAction.Saved);
        }

        private bool CheckForSubjectCreation() {
            return (subjectProcessor.Get().Count == 0
                    && AskForElementCreation("Sie haben noch keine Fächer eingetragen. Möchten Sie jetzt ein Fach hinzufügen?", "Fach hinzufügen?") == MessageBoxResult.Yes
                    && OpenAddSubject() == BusinessObjectManagerAction.Saved);
        }

        private MessageBoxResult AskForElementCreation(string question, string caption) {
            return MessageBox.Show(question, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        }


        private void UpdateSubjects() {
            // Get business objects
            var businessObjects = new List<Subject>();
            var businessObjectParents = teacherProcessor.Get(ApplicationPropertyManager.Workspace.School);
            foreach (var teacher in businessObjectParents) {
                businessObjects.AddRange(subjectProcessor.Get(teacher));
            }

            // Display business objects
            if (businessObjects.Count() > 0) {
                icSubjects.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Es sind noch keine Fächer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                icSubjects.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }

            // Update controls
            // TODO: __
            if (subjectProcessor.CanCreate(ApplicationPropertyManager.Workspace.School)) {
                btnManageSubjects.IsEnabled = true;
                btnManageSubjects.ToolTip = null;
            }
            else {
                btnManageSubjects.IsEnabled = false;
                btnManageSubjects.ToolTip = "Mindestens ein Lehrer muss existieren, um Fächer verwalten zu können!";
            }
        }


        private void UpdateUpcommingEvents() {
            // Get business objects
            var allEvents = new List<Event>();

            allEvents.AddRange(UpdateUpcommingEventsThisWeek());
            allEvents.AddRange(UpdateUpcommingEventsNextWeek());

            // Display business objects
            if (allEvents.Count > 0) {
                lblNoUpcommingEvents.Visibility = System.Windows.Visibility.Collapsed;
                gUpcommingEventsOverview.Visibility = System.Windows.Visibility.Visible;
            }
            else {
                lblNoUpcommingEvents.Visibility = System.Windows.Visibility.Visible;
                gUpcommingEventsOverview.Visibility = System.Windows.Visibility.Collapsed;
            }

            // Update controls
            // TODO: __
            if (eventProcessor.CanCreate(ApplicationPropertyManager.Workspace.School)) {
                btnManageEvents.IsEnabled = true;
                btnManageEvents.ToolTip = null;
            }
            else {
                btnManageEvents.IsEnabled = false;
                btnManageEvents.ToolTip = "Mindestens ein Fach muss existieren, um Ereignisse verwalten zu können!";
            }
        }

        private List<Event> UpdateUpcommingEvents(Panel container, ItemsControl itemsContainer, DateTime startDate, DateTime endDate) {
            List<Event> events = ((BusinessLogic.EventProcessor)eventProcessor).Get(startDate, endDate);

            if (events.Count > 0) {
                itemsContainer.ItemsSource = events;
                container.Visibility = System.Windows.Visibility.Visible;
            }
            else {
                container.Visibility = System.Windows.Visibility.Collapsed;
            }

            return events;
        }

        private List<Event> UpdateUpcommingEventsThisWeek() {
            var today = DateTime.Today;
            return UpdateUpcommingEvents(gUpcommingEventsThisWeek, icUpcommingEventsThisWeek, today, today.GetLastDayOfWeek());
        }

        private List<Event> UpdateUpcommingEventsNextWeek() {
            var nextWeek = DateTime.Today.AddDays(7);
            return UpdateUpcommingEvents(gUpcommingEventsNextWeek, icUpcommingEventsNextWeek, nextWeek.GetFirstDayOfWeek(), nextWeek.GetLastDayOfWeek());
        }


        private void OpenManageDocuments() {
            var managementWindow = new ManageDocuments() { Owner = this };
            managementWindow.ShowDialog();
        }

        private void OpenManageNotes() {
            var managementWindow = new ManageNotes() { Owner = this };
            managementWindow.ShowDialog();
        }

        private void OpenManageGrades() {
            var managementWindow = new ManageGrades() { Owner = this };
            managementWindow.ShowDialog();
        }

        private void OpenManageEvents() {
            var managementWindow = new ManageEvents() { Owner = this };
            managementWindow.ShowDialog();
        }


        private BusinessObjectManagerAction OpenAddDocument() {
            var businessObjectManagerWindow = new ManageDocument(ApplicationPropertyManager.Workspace.Subject);
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddNote() {
            var businessObjectManagerWindow = new ManageNote(ApplicationPropertyManager.Workspace.Subject) { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddGrade() {
            // TODO:
            return BusinessObjectManagerAction.Undefined;

            //var businessObjectManagerWindow = new ManageGrade(ApplicationPropertyManager.Workspace.Subject) { Owner = this };
            //businessObjectManagerWindow.ShowDialog();
            //return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddEvent() {
            // TODO:
            //return BusinessObjectManagerAction.Undefined;

            var businessObjectManagerWindow = new ManageEvent(DateTime.Today) { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddTeacher() {
            var businessObjectManagerWindow = new ManageTeacher() { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddSubject() {
            var businessObjectManagerWindow = new ManageSubject() { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }


        private void UpdateManageSubjectsAvailability() {

        }

        private void OpenManageSubjects() {
            Window managementWindow = new ManageSubjects();
            managementWindow.Show();
            Close();
        }

        private void OpenManageTeachers() {
            Window managementWindow = new ManageTeachers();
            managementWindow.Show();
            Close();
        }

        private void OpenManageSchools() {
            Window managementWindow = new ManageSchools();
            managementWindow.Show();
            Close();
        }

        #endregion

        #region Event wiring

        private void wSchoolOverview_Loaded(object sender, RoutedEventArgs e) {
            UpdateSubjects();
            UpdateUpcommingEvents();

            CheckForTeacherCreation();
            CheckForSubjectCreation();
        }


        private void SubjectOverview_ManagingDocuments(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageDocuments();
        }
        private void SubjectOverview_AddingDocument(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddDocument() == BusinessObjectManagerAction.Saved) {
                UpdateSubjects();
            }
        }

        private void SubjectOverview_ManagingNotes(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageNotes();
        }
        private void SubjectOverview_AddingNote(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddNote() == BusinessObjectManagerAction.Saved) {
                UpdateSubjects();
            }
        }

        private void SubjectOverview_ManagingGrades(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageGrades();
        }
        private void SubjectOverview_AddingGrade(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddGrade() == BusinessObjectManagerAction.Saved) {
                UpdateSubjects();
            }
        }

        private void SubjectOverview_ManagingEvents(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageEvents();
        }
        private void SubjectOverview_AddingEvent(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddEvent() == BusinessObjectManagerAction.Saved) {
                UpdateSubjects();
                UpdateUpcommingEvents();
            }
        }


        private void btnManageSubjects_Click(object sender, RoutedEventArgs e) {
            //if (subjectProcessor.CanCreate()) {
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Lehrer muss existieren, um Fächer verwalten zu können!", "Keine existierenden Lehrer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}

            OpenManageSubjects();
            UpdateSubjects();
        }

        private void btnManageTeachers_Click(object sender, RoutedEventArgs e) {
            OpenManageTeachers();
            UpdateSubjects();
        }

        private void btnChangeSchool_Click(object sender, RoutedEventArgs e) {
            ApplicationPropertyManager.Workspace = null;

            OpenManageSchools();
        }


        private void btnManageEvents_Click(object sender, RoutedEventArgs e) {
            var window = new ManageEvents();
            window.ShowDialog();
        }

        // TODO: Enhance this temporary solution
        #region Temp

        private void btnGrades_Click(object sender, RoutedEventArgs e) {
            //if (gradeProcessor.CanCreate()) {
            var window = new ManageGrades();
            window.ShowDialog();
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Fach muss existieren, um Noten verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}            
        }

        #endregion

        #endregion

    }

}
