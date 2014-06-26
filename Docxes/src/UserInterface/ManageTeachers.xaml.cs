using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                Common.UpdateSelectedItem(lbTeachers);
            }
            else {
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Es sind noch keine Lehrer an dieser Schule vorhanden.\nKlicken Sie auf \"Hinzufügen\" um einen neuen Lehrer zu erstellen.");
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
            try {
                UpdateBusinessObjects();
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void wManageTeachers_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
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


        private void lbTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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
