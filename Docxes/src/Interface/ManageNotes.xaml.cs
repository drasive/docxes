using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageNotes.xaml"/>
    /// </summary>
    public sealed partial class ManageNotes : Window {

        private BusinessLogic.BusinessObjectProcessor<Subject> businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.BusinessObjectProcessor<Note> businessObjectProcessor = new BusinessLogic.NoteProcessor();


        public ManageNotes() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubjects.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get();
            cbSubjects.ItemsSource = businessObjectParents;
            cbSubjects.SelectedIndex = 0;
        }


        private void UpdateBusinessObjects() {
            IEnumerable<Note> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbNotes.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Notizen für dieses Fach gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Notiz zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbNotes.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageNote(SelectedBusinessObjectParent) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageNote(SelectedBusinessObjectParent, (Note)lbNotes.SelectedItem) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Notiz wirklich löschen?", "Notiz")) {
                businessObjectProcessor.Delete((Note)lbNotes.SelectedItem);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            bool isBusinessObjectSelected = lbNotes.SelectedIndex != -1;

            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = isBusinessObjectSelected;
            }
        }

        #endregion

        #region Event wiring

        private void wManageNotes_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjectParents();
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void lbNotes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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
