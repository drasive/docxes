using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="SchoolOverview.xaml"/>
    /// </summary>
    public partial class SchoolOverview : Window {

        // ASK: Better solution than to create instances everywhere but still have inheritance and stuff for the processors?
        private BusinessLogic.BusinessObjectProcessor<Teacher, School> teacherProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> subjectProcessor = new BusinessLogic.SubjectProcessor();

        private BusinessLogic.BusinessObjectProcessor<Document, Subject> documentProcessor = new BusinessLogic.DocumentProcessor();
        private BusinessLogic.BusinessObjectProcessor<Note, Subject> noteProcessor = new BusinessLogic.NoteProcessor();
        private BusinessLogic.BusinessObjectProcessor<Grade, Subject> gradeProcessor = new BusinessLogic.GradeProcessor();


        public SchoolOverview() {
            InitializeComponent();

            tbTitle.Text = ApplicationPropertyManager.Workspace.School.Name;

            // TODO: Check if there are subjects and ask to create
        }

        #region Control

        // TODO: Business logic
        private void UpdateWorkspace(Teacher teacher) {
            ApplicationPropertyManager.Workspace.Teacher = teacher;
        }

        private void UpdateWorkspace(Subject subject) {
            ApplicationPropertyManager.Workspace.Subject = subject;
        }

        #endregion

        #region Interface

        private void UpdateBusinessObjects() {
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
                // ASK: Move into BusinessLogic somehow?
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Es sind noch keine Fächer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                icSubjects.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
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

            var businessObjectManagerWindow = new ManageEvent(ApplicationPropertyManager.Workspace.Subject) { Owner = this };
            businessObjectManagerWindow.ShowDialog();
            return ((IBusinessObjectManager)businessObjectManagerWindow).Action;
        }


        private void OpenManageSchools() {
            Window managementWindow = new ManageSchools();
            managementWindow.Show();
            Close();
        }

        #endregion

        #region Event wiring

        private void wSchoolOverview_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
        }


        private void btnManageDocuments_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageDocuments();
        }

        private void btnManageNotes_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageNotes();
        }

        private void btnManageGrades_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageGrades();
        }

        private void btnManageEvents_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            OpenManageEvents();
        }


        private void btnAddDocument_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddDocument() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddNote() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnAddGrade_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddGrade() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e) {
            var senderControl = (Control)sender;
            var businessObject = (Subject)senderControl.DataContext;

            UpdateWorkspace(businessObject.Teacher);
            UpdateWorkspace(businessObject);
            if (OpenAddEvent() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }


        private void btnChangeSchool_Click(object sender, RoutedEventArgs e) {
            ApplicationPropertyManager.Workspace = null;

            Window schoolSelection = new ManageSchools();
            schoolSelection.Show();
            Close();
        }

        // TODO: Enhance this temporary solution
        #region Temp

        private void btnDocuments_Click(object sender, RoutedEventArgs e) {
            //if (documentProcessor.CanCreate()) {
            var window = new ManageDocuments();
            window.ShowDialog();
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Fach muss existieren, um Dokumente verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e) {
            var window = new ManageEvents();
            window.ShowDialog();
        }

        private void btnGrades_Click(object sender, RoutedEventArgs e) {
            //if (gradeProcessor.CanCreate()) {
            var window = new ManageGrades();
            window.ShowDialog();
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Fach muss existieren, um Noten verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}            
        }

        private void btnNotes_Click(object sender, RoutedEventArgs e) {
            //if (noteProcessor.CanCreate()) {
            var window = new ManageNotes();
            window.ShowDialog();
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Fach muss existieren, um Notizen verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}
        }

        private void btnSubjects_Click(object sender, RoutedEventArgs e) {
            //if (subjectProcessor.CanCreate()) {
            var window = new ManageSubjects();
            window.ShowDialog();
            //}
            //else {
            //    MessageBox.Show("Mindestens ein Lehrer muss existieren, um Fächer verwalten zu können!", "Keine existierenden Lehrer", MessageBoxButton.OK, MessageBoxImage.Hand);
            //}


            UpdateBusinessObjects();
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e) {
            var window = new ManageTeachers();
            window.ShowDialog();


            UpdateBusinessObjects();
        }

        #endregion       

        #endregion

    }

}
