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

        public SchoolOverview() {
            InitializeComponent();

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
            var window = new ManageDocuments();
            window.ShowDialog();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e) {
            var window = new ManageEvents();
            window.ShowDialog();
        }

        private void btnGrades_Click(object sender, RoutedEventArgs e) {
            var window = new ManageGrades();
            window.ShowDialog();
        }

        private void btnNotes_Click(object sender, RoutedEventArgs e) {
            var window = new ManageNotes();
            window.ShowDialog();
        }

        private void btnSubjects_Click(object sender, RoutedEventArgs e) {
            var window = new ManageSubjects();
            window.ShowDialog();
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e) {
            var window = new ManageTeachers();
            window.ShowDialog();
        }

        #endregion

    }

}
