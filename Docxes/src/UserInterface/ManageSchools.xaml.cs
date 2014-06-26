using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchools.xaml"/>.
    /// </summary>
    internal sealed partial class ManageSchools : Window {

        private BusinessLogic.SchoolProcessor businessObjectProcessor = new BusinessLogic.SchoolProcessor();


        internal ManageSchools() {
            InitializeComponent();

            Common.ExtendWindowName(this);
            tblWelcomeText.Text = "Willkommen, " + System.Environment.UserName + "!";
        }

        #region Interface

        private School SelectedBusinessObject { get { return (School)lbSchools.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<School> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbSchools.ItemsSource = businessObjects;

                if (lbSchools.SelectedIndex == -1) {
                    lbSchools.SelectedIndex = 0;
                }
                else {
                    Common.UpdateSelectedItem(lbSchools);
                }
            }
            else {
                var noBusinessObjectsPlaceholder = Common.GeneratePlaceholderListBoxItem("Es sind noch keine Schulen vorhanden.\nKlicken Sie auf \"Hinzufügen\" um eine neue Schule zu erstellen.");
                lbSchools.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private void OpenSchoolOverview(School school) {
            ApplicationPropertyManager.Workspace = new Workspace(SelectedBusinessObject);

            Window schoolOverview = new SchoolOverview();
            schoolOverview.Show();
            Close();
        }

        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            ManageSchool addBusinessObjectManager = new ManageSchool() { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return addBusinessObjectManager.Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            ManageSchool editBusinessObjectManager = new ManageSchool(SelectedBusinessObject) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return editBusinessObjectManager.Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Schule und alle zugehörigen Daten (Lehrer, Fächer, Ereignisse, Dokumente, Notizen und Noten) wirklich löschen?", "Schule")) {
                businessObjectProcessor.Delete(SelectedBusinessObject);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnSelect, btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
            }
        }

        #endregion

        #region Event wiring

        private void wManageSchools_Loaded(object sender, RoutedEventArgs e) {
            try {
                UpdateBusinessObjects();
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void wManageSchools_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            try {
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H) {
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


        private void lbSchools_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            try {
                OpenSchoolOverview(SelectedBusinessObject);
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
