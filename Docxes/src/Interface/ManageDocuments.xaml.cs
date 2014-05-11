using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    // TODO: Interface

    /// <summary>
    /// Interaction logic for <see cref="ManageDocuments.xaml"/>
    /// </summary>
    public sealed partial class ManageDocuments : Window {

        private BusinessLogic.BusinessObjectProcessor<Document> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        public ManageDocuments() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private void UpdateBusinessObjects() {
            IEnumerable<Document> businessObjects = businessObjectProcessor.Get();

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
            //TODO:
            IBusinessObjectManager addBusinessObjectManager = new ManageDocument() { Owner = this };
            //addBusinessObjectManager.ShowDialog();
            return addBusinessObjectManager.Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            // TODO:
            IBusinessObjectManager editBusinessObjectManager = new ManageDocument((Document)lbSchools.SelectedItem) { Owner = this };
            //editBusinessObjectManager.ShowDialog();
            return editBusinessObjectManager.Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie dieses Dokument wirklich löschen?", "Dokument")) {
                businessObjectProcessor.Delete((Document)lbSchools.SelectedItem);
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

        private void wManageDocuments_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        // TODO
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
