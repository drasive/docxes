using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageEvents.xaml"/>
    /// </summary>
    public sealed partial class ManageSubjects : Window {

        private BusinessLogic.BusinessObjectProcessor<Teacher> businessObjectParentProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject> businessObjectProcessor = new BusinessLogic.SubjectProcessor();


        public ManageSubjects() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        // ASK: Use property or method?
        private Teacher SelectedBusinessObjectParent { get { return (Teacher)lbTeachers.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Teacher> businessObjectParents = businessObjectParentProcessor.Get();
            lbTeachers.DataContext = businessObjectParents;
            // TODO: _
            //lbTeachers.SelectedIndex = 0;
        }


        private void UpdateBusinessObjects() {
            IEnumerable<Subject> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbSubjects.DataContext = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Fächer für diesen Lehrer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbSubjects.DataContext = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageSubject(SelectedBusinessObjectParent) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageSubject(SelectedBusinessObjectParent, (Subject)lbSubjects.SelectedItem) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie dieses Fach und alle zugehörigen Daten (Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Dokument")) {
                businessObjectProcessor.Delete((Subject)lbSubjects.SelectedItem);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            bool isBusinessObjectSelected = lbSubjects.SelectedIndex != -1;

            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = isBusinessObjectSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageSubjects_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjectParents();
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void lbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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
