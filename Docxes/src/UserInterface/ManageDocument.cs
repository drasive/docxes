using System;

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
            Nullable<bool> fileSelected = openFileDialog.ShowDialog();

            if (fileSelected.GetValueOrDefault()) {
                string filePath = openFileDialog.FileName;
                var businessObjectToCreate = new Document(filePath, businessObjectParent);

                businessObjectProcessor.Create(businessObjectToCreate);

                Action = BusinessObjectManagerAction.Saved;
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
            Nullable<bool> fileSelected = openFileDialog.ShowDialog();

            if (fileSelected.GetValueOrDefault()) {
                string filePath = openFileDialog.FileName;
                var businessObjectToCreate = new Document(filePath, businessObjectParent, businessObjectEditing);

                businessObjectProcessor.Update(businessObjectToCreate);

                Action = BusinessObjectManagerAction.Saved;
            }
            else {
                Action = BusinessObjectManagerAction.Canceled;
            }
        }


        internal void ShowDialog() {
            if (Action != BusinessObjectManagerAction.Undefined) {
                throw new InvalidOperationException("Already executed");
            }

            if (IsEditing) {
                Edit();
            }
            else {
                Add();
            }
        }

        #endregion

    }

}
