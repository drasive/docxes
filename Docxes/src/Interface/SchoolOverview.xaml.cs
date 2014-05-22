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

        private School school;

        // ASK: Better solution than to create instances everywhere but have inheritance?
        private BusinessLogic.BusinessObjectProcessor<Teacher> teacherProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject> subjectProcessor = new BusinessLogic.SubjectProcessor();

        private BusinessLogic.BusinessObjectProcessor<Document> documentProcessor = new BusinessLogic.DocumentProcessor();
        private BusinessLogic.BusinessObjectProcessor<Note> noteProcessor = new BusinessLogic.NoteProcessor();
        private BusinessLogic.BusinessObjectProcessor<Grade> gradeProcessor = new BusinessLogic.GradeProcessor();


        public SchoolOverview(School school) {
            InitializeComponent();

            this.school = school;

            // TODO: Check if there are subjects and ask to create
        }

        #region Processing



        #endregion

        #region Interface

        //private void Open1() {
        //    // TODO
        //    Window newWindow = new ManageSchools();
        //    newWindow.ShowDialog();
        //}

        //private void Open2() {
        //    // TODO
        //    Window newWindow = new ManageSchools();
        //    newWindow.ShowDialog();
        //}

        private void OpenManageSchools() {
            Window newWindow = new ManageSchools();
            newWindow.Show();
            Close();
        }

        #endregion

        #region Event wiring

        // TODO: Enhance this temporary solution

        private void btnDocuments_Click(object sender, RoutedEventArgs e) {
            if (documentProcessor.AreRequirementsMetToCreate()) {
                var window = new ManageDocuments();
                window.ShowDialog();
            }
            else {
                MessageBox.Show("Mindestens ein Fach muss existieren, um Dokumente verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e) {
            var window = new ManageEvents();
            window.ShowDialog();
        }

        private void btnGrades_Click(object sender, RoutedEventArgs e) {
            if (gradeProcessor.AreRequirementsMetToCreate()) {
                var window = new ManageGrades();
                window.ShowDialog();
            }
            else {
                MessageBox.Show("Mindestens ein Fach muss existieren, um Noten verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            }            
        }

        private void btnNotes_Click(object sender, RoutedEventArgs e) {
            if (noteProcessor.AreRequirementsMetToCreate()) {
                var window = new ManageNotes();
                window.ShowDialog();
            }
            else {
                MessageBox.Show("Mindestens ein Fach muss existieren, um Notizen verwalten zu können!", "Keine existierenden Fächer", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void btnSubjects_Click(object sender, RoutedEventArgs e) {
            if (subjectProcessor.AreRequirementsMetToCreate()) {
                var window = new ManageSubjects();
                window.ShowDialog();
            }
            else {
                MessageBox.Show("Mindestens ein Lehrer muss existieren, um Fächer verwalten zu können!", "Keine existierenden Lehrer", MessageBoxButton.OK, MessageBoxImage.Hand);
            }            
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e) {
            var window = new ManageTeachers(school);
            window.ShowDialog();
        }

        #endregion

    }

}
