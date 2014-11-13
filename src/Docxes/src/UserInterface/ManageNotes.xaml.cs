using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageNotes.xaml"/>.
    /// </summary>
    internal sealed partial class ManageNotes : Window {

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.NoteProcessor businessObjectProcessor = new BusinessLogic.NoteProcessor();


        internal ManageNotes() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubjects.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get(ApplicationPropertyManager.Workspace.School);

            cbSubjects.ItemsSource = businessObjectParents;
            if (ApplicationPropertyManager.Workspace.Subject != null) {
                cbSubjects.SelectedValue = ApplicationPropertyManager.Workspace.Subject.Id;
            }
            else {
                cbSubjects.SelectedIndex = 0;
            }
        }


        private Note SelectedBusinessObject { get { return (Note)lbNotes.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Note> businessObjects = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            if (businessObjects.Count() > 0) {
                lbNotes.ItemsSource = businessObjects;
                Common.UpdateSelectedItem(lbNotes);
            }
            else {
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Es sind noch keine Notizen für dieses Fach vorhanden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Notiz zu erstellen.");
                lbNotes.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageNote(SelectedBusinessObjectParent) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageNote(SelectedBusinessObjectParent, SelectedBusinessObject) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Notiz wirklich löschen?", "Notiz")) {
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

        private void wManageNotes_Loaded(object sender, RoutedEventArgs e) {
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

        private void wManageNotes_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            try {
                if (e.Key == Key.Escape) {
                    Close();
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H) {
                    if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                        UpdateBusinessObjects();
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B
                         && SelectedBusinessObject != null) {
                    if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                        UpdateBusinessObjects();
                    }
                }
                else if (e.Key == Key.Delete
                         && SelectedBusinessObject != null) {
                    if (CheckForElementDeletion()) {
                        UpdateBusinessObjects();
                    }
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void cbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateBusinessObjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void lbNotes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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
