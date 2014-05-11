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



        #endregion

    }

}
