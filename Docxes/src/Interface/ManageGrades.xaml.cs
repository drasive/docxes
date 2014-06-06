using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    // TODO: Interface

    /// <summary>
    /// Interaction logic for <see cref="ManageGrades.xaml"/>
    /// </summary>
    public sealed partial class ManageGrades : Window {

        private BusinessLogic.BusinessObjectProcessor<Grade, Subject> businessObjectProcessor = new BusinessLogic.GradeProcessor();


        public ManageGrades() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private void UpdateBusinessObjects() {
            IEnumerable<Grade> businessObjects = null; //businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbSchools.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    // TODO:
                    Content = "Keine Schulen gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbSchools.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            // TODO:
            IBusinessObjectManager addBusinessObjectManager = new ManageGrade() { Owner = this };
            //addBusinessObjectManager.ShowDialog();
            return addBusinessObjectManager.Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            // TODO:
            IBusinessObjectManager editBusinessObjectManager = new ManageGrade((Grade)lbSchools.SelectedItem) { Owner = this };
            //editBusinessObjectManager.ShowDialog();
            return editBusinessObjectManager.Action;
        }

        private bool CheckForElementDeletion() {
            // TODO:
            if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Graden) wirklich löschen?", "Schule")) {
                businessObjectProcessor.Delete((Grade)lbSchools.SelectedItem);
                return true;
            }

            return false;
        }
        
        private void UpdateControlsAvailability() {
            bool isBusinessObjectSelected = lbSchools.SelectedIndex != -1;
            
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = isBusinessObjectSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageGrades_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        // TODO:
        private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
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
