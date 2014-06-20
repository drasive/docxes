using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageTeachers.xaml"/>.
    /// </summary>
    internal sealed partial class ManageTeachers : Window {

        private School businessObjectParent { get { return ApplicationPropertyManager.Workspace.School; } }

        private BusinessLogic.TeacherProcessor businessObjectProcessor = new BusinessLogic.TeacherProcessor();


        internal ManageTeachers() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Teacher SelectedBusinessObject { get { return (Teacher)lbTeachers.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Teacher> businessObjects = businessObjectProcessor.Get(businessObjectParent);

            if (businessObjects.Count() > 0) {
                lbTeachers.ItemsSource = businessObjects;
            }
            else {
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Keine Lehrer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um einen neuen Lehrer zu erstellen.");
                lbTeachers.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageTeacher() { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageTeacher(SelectedBusinessObject) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diesen Lehrer und alle zugehörigen Daten (Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Lehrer")) {
                businessObjectProcessor.Delete(SelectedBusinessObject);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
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
