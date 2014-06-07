using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageDocuments.xaml"/>
    /// </summary>
    public sealed partial class ManageDocuments : Window {

        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.BusinessObjectProcessor<Document, Subject> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        public ManageDocuments() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubjects.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get();

            cbSubjects.ItemsSource = businessObjectParents;
            if (ApplicationPropertyManager.Workspace.Subject != null) {
                cbSubjects.SelectedValue = ApplicationPropertyManager.Workspace.Subject.Id;
            }
            else {
                cbSubjects.SelectedIndex = 0;
            }
        }


        private Document SelectedBusinessObject { get { return (Document)lbDocuments.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Document> businessObjects = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            if (businessObjects.Count() > 0) {
                lbDocuments.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Dokumente gefunden.\nKlicken Sie auf \"Hinzufügen\" um ein Dokument hinzuzufügen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbDocuments.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
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
            UpdateBusinessObjectParents();
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void cbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateBusinessObjects();
        }


        private void lbDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e) {
            Process.Start(SelectedBusinessObject.FilePath);
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e) {
            Process.Start("explorer.exe", "/select," + SelectedBusinessObject.FilePath);
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
