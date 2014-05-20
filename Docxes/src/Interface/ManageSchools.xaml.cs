using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchools.xaml"/>
    /// </summary>
    public sealed partial class ManageSchools : Window {

        // TODO:_
        private BusinessLogic.BusinessObjectProcessor<School> businessObjectProcessor = new BusinessLogic.SchoolProcessor();


        public ManageSchools() {
            InitializeComponent();

            Common.ExtendWindowName(this);
            tblWelcomeText.Text = "Willkommen, " + System.Environment.UserName + "!";
        }

        #region Interface

        private void UpdateBusinessObjects() {
            IEnumerable<School> businessObjects = businessObjectProcessor.Get();
            //IEnumerable<School> businessObjects = new List<School>();

            if (businessObjects.Count() > 0) {
                lbSchools.DataContext = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Schulen gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbSchools.DataContext = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private void OpenSchoolOverview() {
            Window newWindow = new SchoolOverview();
            newWindow.Show();
            Close();
        }

        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            ManageSchool addBusinessObjectManager = new ManageSchool() { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return addBusinessObjectManager.Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            ManageSchool editBusinessObjectManager = new ManageSchool((School)lbSchools.SelectedItem) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return editBusinessObjectManager.Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Schule")) {
                //businessObjectProcessor.Delete((School)lbSchools.SelectedItem);
                return true;
            }

            return false;
        }
        
        private void UpdateControlsAvailability() {
            bool isBusinessObjectSelected = lbSchools.SelectedIndex != -1;
            
            foreach (Button button in new Button[] { btnSelect, btnEdit, btnDelete }) {
                button.IsEnabled = isBusinessObjectSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageSchools_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            OpenSchoolOverview();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (CheckForElementDeletion()) {
                UpdateBusinessObjects();
            }
        }

        #endregion

    }

}
