using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    // TODO: Update formatting (application wide)
    // TODO: Update document name after entity update

    /// <summary>
    /// Interaction logic for <see cref="ManageDocuments.xaml"/>.
    /// </summary>
    internal sealed partial class ManageDocuments : Window {

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.DocumentProcessor businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        internal ManageDocuments() {
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


        private Document SelectedBusinessObject { get { return (Document)lvDocuments.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Document> businessObjects = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            lvDocuments.ItemsSource = businessObjects;
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            ManageDocument addBusinessObjectManager = new ManageDocument(SelectedBusinessObjectParent);
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            ManageDocument editBusinessObjectManager = new ManageDocument(SelectedBusinessObjectParent, SelectedBusinessObject);
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie die Verknüpfung zu diesem Dokument wirklich löschen? Das Dokument selbst wird dabei nicht gelöscht.", "Verknüpfung zu Dokument")) {
                businessObjectProcessor.Delete(SelectedBusinessObject);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnOpen, btnOpenFolder, btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
            }
        }

        #endregion

        #region Event wiring

        private void wManageDocuments_Loaded(object sender, RoutedEventArgs e) {
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

        private void wManageDocuments_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            try {
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H) {
                    if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                        UpdateBusinessObjects();
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O
                         && SelectedBusinessObject != null) {
                    Process.Start(SelectedBusinessObject.FilePath);
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && Keyboard.Modifiers == ModifierKeys.Shift && e.Key == Key.O
                         && SelectedBusinessObject != null) {
                    Process.Start("explorer.exe", "/select," + SelectedBusinessObject.FilePath);
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


        private void lvDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e) {
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

        private void btnOpen_Click(object sender, RoutedEventArgs e) {
            try {
                Process.Start(SelectedBusinessObject.FilePath);
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e) {
            try {
                Process.Start("explorer.exe", "/select," + SelectedBusinessObject.FilePath);
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
