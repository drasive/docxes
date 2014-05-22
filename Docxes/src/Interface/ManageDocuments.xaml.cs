using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageDocuments.xaml"/>
    /// </summary>
    public sealed partial class ManageDocuments : Window {

        private BusinessLogic.BusinessObjectProcessor<Subject> businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.BusinessObjectProcessor<Document> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        public ManageDocuments() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubject.SelectedItem; } }
        // TODO: Use in all classes like this
        private Document SelectedBusinessObject { get { return (Document)lbDocuments.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Document> businessObjects = businessObjectProcessor.Get();

            if (businessObjects.Count() > 0) {
                lbDocuments.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Dokumente gefunden.\nKlicken Sie auf \"Hinzufügen\" um eine neues Dokument zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbDocuments.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie die Verknüpfung zu diesem Dokument wirklich löschen? Das Dokument selbst wird dabei nicht gelöscht.", "Verknüpfung zu Dokument")) {
                businessObjectProcessor.Delete((Document)lbDocuments.SelectedItem);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnOpen, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
            }
        }

        #endregion

        #region Event wiring

        private void wManageDocuments_Loaded(object sender, RoutedEventArgs e) {
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void lbDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnOpenFolder_Click(object sender, RoutedEventArgs e) {
            if (SelectedBusinessObject != null) {
                // TODO:
                Process.Start("explorer.exe", "/select," + "FILESTOSELECT");
            }
            else {
                // TODO: 
                Process.Start("explorer.exe", "FOLDER");
            }            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".docx";
            openFileDialog.Filter = @"Word documents (.docx)|*.docx
                                     |Text documents (.txt)|*.txt
                                     |All (.*)|*.*";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.ValidateNames = true;

            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true) {
                // TODO:
                string filename = openFileDialog.FileName;
                
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e) {
            // TODO:
            Process.Start("FILENAME");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            // TODO:
        }

        #endregion

    }

}
