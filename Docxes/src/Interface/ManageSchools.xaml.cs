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
						tblWelcomeText.Text = "Willkommen " + System.Environment.UserName + "!";
            Common.ExtendWindowName(this);
        }

        #region Interface

        private IEnumerable<School> GetElements() {
            Data.ManagementElementController<School> controller = new Data.SchoolsController();
            return new List<School>();

            // TODO: Use
            //return controller.Get();
        }

        private void UpdateElements() {
            IEnumerable<School> elements = GetElements();

            lbSchools.Items.Clear();
            if (elements.Count() > 0) {
                lbSchools.DataContext = elements;
            }
            else {
                ListBoxItem noElementsPlaceholder = new ListBoxItem();
                noElementsPlaceholder.Content = "Keine Schulen gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.";
                noElementsPlaceholder.FontSize = 10;
                noElementsPlaceholder.IsEnabled = false;
                lbSchools.Items.Add(noElementsPlaceholder);
            }
        }


        private ManagementElementManagerAction OpenAddElementManager() {
            ManageSchool addElementManager = new ManageSchool();
            addElementManager.Owner = this;
            addElementManager.ShowDialog();
            return addElementManager.Action;
        }

        private ManagementElementManagerAction OpenEditElementManager() {
            ManageSchool editElementManager = new ManageSchool(); // TODO: Extend
            editElementManager.Owner = this;
            editElementManager.ShowDialog();
            return editElementManager.Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Schule")) {

                return true;
            }

            return false;
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
            UpdateElements(); ;
            UpdateControlsAvailability();
        }


        private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            // TODO: Implement
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            if (OpenAddElementManager() == ManagementElementManagerAction.Saved) {
                UpdateElements();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            if (OpenEditElementManager() == ManagementElementManagerAction.Saved) {
                UpdateElements();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (CheckForElementDeletion()) {
                UpdateElements();
            }
        }

        #endregion

    }
}
