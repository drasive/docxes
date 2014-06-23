using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSubjects.xaml"/>.
    /// </summary>
    internal sealed partial class ManageSubjects : Window {

        private BusinessLogic.TeacherProcessor businessObjectParentProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.SubjectProcessor businessObjectProcessor = new BusinessLogic.SubjectProcessor();


        internal ManageSubjects() {
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
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Es sind noch keine Fächer für diesen Lehrer vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Fach zu erstellen.");
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
            try {
                UpdateBusinessObjectParents();
                UpdateBusinessObjects();
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void cbTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateBusinessObjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void lbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                if (CheckForElementDeletion()) {
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        #endregion

    }

}
