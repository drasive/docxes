using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Provides functionality to manage an entity of the type <see cref="Document"/>.
    /// </summary>
    internal partial class ManageDocument : IBusinessObjectManager {

        private Subject businessObjectParent;
        private Document businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Document, Subject> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        private void Initialize(Subject businessObjectParent, Document businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;
        }

        internal ManageDocument(Subject businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        internal ManageDocument(Subject businessObjectToEditParent, Document businessObjectToEdit) {
            if (businessObjectToEditParent == null) {
                throw new ArgumentNullException("businessObjectToEditParent");
            }
            if (businessObjectToEdit == null) {
                throw new ArgumentNullException("businessObjectToEdit");
            }

            Initialize(businessObjectToEditParent, businessObjectToEdit);
        }


        public bool IsEditing { get { return businessObjectEditing != null; } }

        public BusinessObjectManagerAction Action { get; private set; }

        #region Control

        private Microsoft.Win32.OpenFileDialog GetOpenFileDialog() {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word documents|*.docx" +
                                    "|Text documents|*.txt" +
                                    "|All files|*.*";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.ValidateNames = true;

            return openFileDialog;
        }


        private void Add() {
            var openFileDialog = GetOpenFileDialog();
            openFileDialog.FilterIndex = 3;
            Nullable<bool> selectedFile = openFileDialog.ShowDialog();

            if (selectedFile.GetValueOrDefault()) {
                if (Save(openFileDialog)) {
                    Action = BusinessObjectManagerAction.Saved;
                }
            }
            else {
                Action = BusinessObjectManagerAction.Canceled;
            }
        }

        private void Edit() {
            var openFileDialog = GetOpenFileDialog();
            openFileDialog.FileName = businessObjectEditing.FilePath;
            switch (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower()) {
                case ".docx":
                    openFileDialog.FilterIndex = 1;
                    break;
                case ".txt":
                    openFileDialog.FilterIndex = 2;
                    break;
                default:
                    openFileDialog.FilterIndex = 3;
                    break;
            }
            Nullable<bool> selectedFile = openFileDialog.ShowDialog();

            if (selectedFile.GetValueOrDefault()) {
                if (Save(openFileDialog)) {
                    Action = BusinessObjectManagerAction.Saved;
                }
            }
            else {
                Action = BusinessObjectManagerAction.Canceled;
            }
        }


        private bool Save(Microsoft.Win32.OpenFileDialog dialog) {
            if (ValidateInput(dialog.FileName)) {
                var businessObjectToSave = MapInterfaceToElement(dialog.FileName);

                if (IsEditing) {
                    businessObjectProcessor.Update(businessObjectToSave);
                }
                else {
                    businessObjectProcessor.Create(businessObjectToSave);
                }
                return true;
            }

            return false;
        }

        #endregion

        #region Interface

        private Document MapInterfaceToElement(string filePath) {
            if (IsEditing) {
                return new Document(filePath, businessObjectParent, businessObjectEditing);
            }
            else {
                return new Document(filePath, businessObjectParent);
            }
        }


        private bool ValidateInput(string filePath) {
            Document duplicate;
            if (IsEditing) {
                duplicate = businessObjectProcessor.Get(businessObjectParent).Find(entity => entity.FilePath.ToUpper() == filePath.ToUpper()
                                                                                             && entity.Id != businessObjectEditing.Id);
            }
            else {
                duplicate = businessObjectProcessor.Get(businessObjectParent).Find(entity => entity.FilePath.ToUpper() == filePath.ToUpper());
            }

            var doesDuplicateExist = duplicate != null;
            if (doesDuplicateExist) {
                MessageBox.Show("Dieses Dokument ist bereits diesem Fach zugeordnet.", "Dokument bereits zugeordnet",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return !doesDuplicateExist;
        }


        internal void ShowDialog() {
            if (Action != BusinessObjectManagerAction.Undefined) {
                throw new InvalidOperationException("Already executed");
            }

            do {
                if (IsEditing) {
                    Edit();
                }
                else {
                    Add();
                }
            } while (Action == BusinessObjectManagerAction.Undefined);
        }

        #endregion

    }

}
