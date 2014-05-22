using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageTeachers.xaml"/>
    /// </summary>
    public sealed partial class ManageTeachers : Window {

        // TODO: Rename
        private School businessObjectParent;

        private BusinessLogic.BusinessObjectProcessor<Teacher> businessObjectProcessor = new BusinessLogic.TeacherProcessor();


        public ManageTeachers(School businessObjectParent) {
            // TODO: Rename parameters

            InitializeComponent();

            this.businessObjectParent = businessObjectParent;

            Common.ExtendWindowName(this);
        }

        #region Interface

        private void UpdateBusinessObjects() {
            IEnumerable<Teacher> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbTeachers.DataContext = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Lehrer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um einen neuen Lehrer zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbTeachers.DataContext = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageTeacher(businessObjectParent) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageTeacher(businessObjectParent, (Teacher)lbTeachers.SelectedItem) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diesen Lehrer und alle zugehörigen Daten (Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Lehrer")) {
                businessObjectProcessor.Delete((Teacher)lbTeachers.SelectedItem);
                return true;
            }

            return false;
        }
        
        private void UpdateControlsAvailability() {
            bool isBusinessObjectSelected = lbTeachers.SelectedIndex != -1;
            
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = isBusinessObjectSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageTeachers_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void lbTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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
