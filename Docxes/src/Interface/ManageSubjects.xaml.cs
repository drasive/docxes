using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageEvents.xaml"/>
    /// </summary>
    public sealed partial class ManageSubjects : Window {

        private BusinessLogic.BusinessObjectProcessor<Teacher, School> businessObjectParentProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> businessObjectProcessor = new BusinessLogic.SubjectProcessor();


        public ManageSubjects() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Teacher SelectedBusinessObjectParent { get { return (Teacher)cbTeachers.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Teacher> businessObjectParents = businessObjectParentProcessor.Get(ApplicationPropertyManager.Workspace.School);

            cbTeachers.ItemsSource = businessObjectParents;
            if (ApplicationPropertyManager.Workspace.Teacher != null) {
                cbTeachers.SelectedValue = ApplicationPropertyManager.Workspace.Teacher.Id;
            }
            else {
                cbTeachers.SelectedIndex = 0;
            }           
        }


        private Subject SelectedBusinessObject { get { return (Subject)lbSubjects.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Subject> businessObjects = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            if (businessObjects.Count() > 0) {
                lbSubjects.ItemsSource = businessObjects;
            }
            else {
                // ASK: Move into BusinessLogic somehow?
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Fächer für diesen Lehrer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbSubjects.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
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
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null; ;
            }
        }

        #endregion

        #region Event wiring

        private void wManageSubjects_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjectParents();
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void cbTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateBusinessObjects();
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
