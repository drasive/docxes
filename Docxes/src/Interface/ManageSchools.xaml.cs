using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchools.xaml"/>
    /// </summary>
    public sealed partial class ManageSchools : Window {

        public ManageSchools() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private void Fill() {
            Data.ManagementObjectController<School> controller = new Data.SchoolsController();
            IEnumerable<School> dataSource = new List<School>(); // TODO: Uncomment
            //IEnumerable<Data.School> dataSource = controller.Get();

            lbSchools.Items.Clear();
            if (dataSource.Count() > 0) {
                lbSchools.DataContext = dataSource;
            }
            else {
                ListBoxItem noObjectsPlaceholder = new ListBoxItem();
                noObjectsPlaceholder.Content = "Keine Schulen gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.";
                noObjectsPlaceholder.IsEnabled = false;
                lbSchools.Items.Add(noObjectsPlaceholder);
            }
        }


        private void OpenAddObjectWindow() {
            ManageSchool addObjectWindow = new ManageSchool();
            addObjectWindow.Owner = this;
            addObjectWindow.ShowDialog();
        }

        private void OpenEditObjectWindow() {
            ManageSchool editObjectWindow = new ManageSchool(); // TODO: Extend
            editObjectWindow.Owner = this;
            editObjectWindow.ShowDialog();
        }

        private void CheckDeleteObject() {
            if (Common.PromptObjectDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Schule")) {

            }
        }

        private void UpdateControlsAvailability() {
            bool isElementSelected = lbSchools.SelectedIndex != -1;

            foreach (Button button in new Button[] { btnSelect, btnEdit, btnDelete }) {
                button.IsEnabled = isElementSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageSchools_Loaded(object sender, RoutedEventArgs e) {
            Fill(); ;
            UpdateControlsAvailability();
        }


        private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            // TODO: Implement
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            OpenAddObjectWindow();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            OpenEditObjectWindow();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            CheckDeleteObject();
        }

        #endregion

    }
}
