using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    // TODO: _Test input validation
    // TODO: _Test edge cases for displaying

    /// <summary>
    /// Interaction logic for <see cref="SchoolOverview.xaml"/>.
    /// </summary>
    internal partial class SchoolOverview : Window {

        private BusinessLogic.TeacherProcessor teacherProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.SubjectProcessor subjectProcessor = new BusinessLogic.SubjectProcessor();

        private BusinessLogic.DocumentProcessor documentProcessor = new BusinessLogic.DocumentProcessor();
        private BusinessLogic.NoteProcessor noteProcessor = new BusinessLogic.NoteProcessor();
        private BusinessLogic.GradeProcessor gradeProcessor = new BusinessLogic.GradeProcessor();
        private BusinessLogic.EventProcessor eventProcessor = new BusinessLogic.EventProcessor();


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
            return (teacherProcessor.Get(ApplicationPropertyManager.Workspace.School).Count == 0
                    && AskForElementCreation("Sie haben noch keine Lehrer eingetragen. Möchten Sie jetzt einen Lehrer hinzufügen?", "Lehrer hinzufügen?") == MessageBoxResult.Yes
                    && OpenAddTeacher() == BusinessObjectManagerAction.Saved);
        }

        private bool CheckForSubjectCreation() {
            var canCreate = subjectProcessor.CanCreate(ApplicationPropertyManager.Workspace.School);
            var subjects = subjectProcessor.Get(ApplicationPropertyManager.Workspace.School);

            return (canCreate
                    && subjects.Count == 0
                    && AskForElementCreation("Sie haben noch keine Fächer eingetragen. Möchten Sie jetzt ein Fach hinzufügen?", "Fach hinzufügen?") == MessageBoxResult.Yes
                    && OpenAddSubject() == BusinessObjectManagerAction.Saved);
        }

        private MessageBoxResult AskForElementCreation(string question, string caption) {
            return MessageBox.Show(question, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        }


        private void UpdateSubjects() {
            // Get business objects
            var businessObjects = subjectProcessor.Get(ApplicationPropertyManager.Workspace.School);

            // Display business objects
            if (businessObjects.Count() > 0) {
                icSubjects.ItemsSource = businessObjects;
            }
            else {
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Es sind noch keine Fächer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.");
                icSubjects.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }

            // Update controls
            btnManageSubjects.IsEnabled = subjectProcessor.CanCreate(ApplicationPropertyManager.Workspace.School);
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
            btnManageEvents.IsEnabled = eventProcessor.CanCreate(ApplicationPropertyManager.Workspace.School);
        }

        private List<Event> UpdateUpcommingEvents(Panel container, ItemsControl itemsContainer, DateTime startDate, DateTime endDate) {
            List<Event> events = eventProcessor.Get(ApplicationPropertyManager.Workspace.School, startDate, endDate);

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
            var businessObjectManagerWindow = new ManageGrade(ApplicationPropertyManager.Workspace.Subject) { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }

        private BusinessObjectManagerAction OpenAddEvent() {
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


        private void OpenManageSubjects() {
            Window managementWindow = new ManageSubjects() { Owner = this };
            managementWindow.ShowDialog();
        }

        private void OpenManageTeachers() {
            Window managementWindow = new ManageTeachers() { Owner = this };
            managementWindow.ShowDialog();
        }

        private void OpenManageSchools() {
            Window managementWindow = new ManageSchools();
            managementWindow.Show();
            Close();
        }

        #endregion

        #region Event wiring

        private void wSchoolOverview_Loaded(object sender, RoutedEventArgs e) {
            try {
                UpdateSubjects();
                UpdateUpcommingEvents();

                if (CheckForTeacherCreation()) {
                    UpdateSubjects();
                    UpdateUpcommingEvents();
                }
                if (CheckForSubjectCreation()) {
                    UpdateSubjects();
                    UpdateUpcommingEvents();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void wSchoolOverview_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            try {
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.F
                    && subjectProcessor.CanCreate(ApplicationPropertyManager.Workspace.School)) {
                    OpenManageSubjects();

                    UpdateSubjects();
                    UpdateUpcommingEvents();
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.L) {
                    OpenManageTeachers();

                    UpdateSubjects();
                    UpdateUpcommingEvents();
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S) {
                    ApplicationPropertyManager.Workspace = null;

                    OpenManageSchools();
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E
                         && eventProcessor.CanCreate(ApplicationPropertyManager.Workspace.School)) {
                    OpenManageEvents();

                    UpdateUpcommingEvents();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void SubjectOverview_ManagingDocuments(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                OpenManageDocuments();

                UpdateSubjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }
        private void SubjectOverview_AddingDocument(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                if (OpenAddDocument() == BusinessObjectManagerAction.Saved) {
                    UpdateSubjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void SubjectOverview_ManagingNotes(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                OpenManageNotes();

                UpdateSubjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }
        private void SubjectOverview_AddingNote(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                if (OpenAddNote() == BusinessObjectManagerAction.Saved) {
                    UpdateSubjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void SubjectOverview_ManagingGrades(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                OpenManageGrades();

                UpdateSubjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }
        private void SubjectOverview_AddingGrade(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                if (OpenAddGrade() == BusinessObjectManagerAction.Saved) {
                    UpdateSubjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void SubjectOverview_ManagingEvents(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                OpenManageEvents();

                UpdateSubjects();
                UpdateUpcommingEvents();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }
        private void SubjectOverview_AddingEvent(object sender, RoutedEventArgs e) {
            try {
                var senderControl = (Control)sender;
                var businessObject = (Subject)senderControl.DataContext;

                UpdateWorkspace(businessObject.Teacher);
                UpdateWorkspace(businessObject);
                if (OpenAddEvent() == BusinessObjectManagerAction.Saved) {
                    UpdateSubjects();
                    UpdateUpcommingEvents();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnManageSubjects_Click(object sender, RoutedEventArgs e) {
            try {
                OpenManageSubjects();

                UpdateSubjects();
                UpdateUpcommingEvents();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnManageTeachers_Click(object sender, RoutedEventArgs e) {
            try {
                OpenManageTeachers();

                UpdateSubjects();
                UpdateUpcommingEvents();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnChangeSchool_Click(object sender, RoutedEventArgs e) {
            try {
                ApplicationPropertyManager.Workspace = null;

                OpenManageSchools();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnManageEvents_Click(object sender, RoutedEventArgs e) {
            try {
                OpenManageEvents();

                UpdateUpcommingEvents();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        #endregion

    }

}
