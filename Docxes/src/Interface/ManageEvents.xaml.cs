using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    // TODO: Interface

    /// <summary>
    /// Interaction logic for <see cref="ManageEvents.xaml"/>
    /// </summary>
    public sealed partial class ManageEvents : Window {

        private BusinessLogic.BusinessObjectProcessor<Event> businessObjectProcessor = new BusinessLogic.EventProcessor();


        public ManageEvents() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private void UpdateBusinessObjects() {
            IEnumerable<Event> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbSchools.DataContext = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    // TODO:
                    Content = "Keine Schulen gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbSchools.DataContext = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            // TODO:
            IBusinessObjectManager addBusinessObjectManager = new ManageEvent() { Owner = this };
            //addBusinessObjectManager.ShowDialog();
            return addBusinessObjectManager.Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            // TODO:
            IBusinessObjectManager editBusinessObjectManager = new ManageEvent((Event)lbSchools.SelectedItem) { Owner = this };
            //editBusinessObjectManager.ShowDialog();
            return editBusinessObjectManager.Action;
        }

        private bool CheckForElementDeletion() {
            // TODO:
            if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Schule")) {
                businessObjectProcessor.Delete((Event)lbSchools.SelectedItem);
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

        private void wManageEvents_Loaded(object sender, RoutedEventArgs e) {
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
