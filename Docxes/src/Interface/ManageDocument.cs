using System;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageDocument.xaml"/>
    /// </summary>
    public partial class ManageDocument : IBusinessObjectManager {

        private Subject businessObjectParent;
        private Document businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Document> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


        private void Initialize(Subject businessObjectParent, Document businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;
        }

        public ManageDocument(Subject businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        public ManageDocument(Subject businessObjectToEditParent, Document businessObjectToEdit) {
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

        protected Microsoft.Win32.OpenFileDialog GetOpenFileDialog() {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word documents|*.docx" +
                                    "|Text documents|*.txt" +
                                    "|All files|*.*";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.ValidateNames = true;

            return openFileDialog;
        }


        public void Add() {
            if (Action != BusinessObjectManagerAction.Undefined) {
                throw new InvalidOperationException("Already executed an action");
            }

            var openFileDialog = GetOpenFileDialog();
            openFileDialog.DefaultExt = ".docx";
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

        public void Edit() {
            if (Action != BusinessObjectManagerAction.Undefined) {
                throw new InvalidOperationException("Already executed an action");
            }

            var openFileDialog = GetOpenFileDialog();            
            openFileDialog.FileName = businessObjectEditing.FilePath;
            // TODO: Desn't work, is still .docx
            openFileDialog.DefaultExt = "*.*";
            Nullable<bool> fileSelected = openFileDialog.ShowDialog();

            if (fileSelected.GetValueOrDefault()) {
                string filePath = openFileDialog.FileName;
                var businessObjectToCreate = new Document(businessObjectEditing, filePath, businessObjectParent);

                businessObjectProcessor.Create(businessObjectToCreate);

                Action = BusinessObjectManagerAction.Saved;
            }
            else {
                Action = BusinessObjectManagerAction.Canceled;
            }            
        }

        #endregion

    }

}
