using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageNote.xaml"/>.
    /// </summary>
    internal partial class ManageNote : Window, IBusinessObjectManager {

        private Subject businessObjectParent;
        private Note businessObjectEditing;

        private BusinessLogic.NoteProcessor businessObjectProcessor = new BusinessLogic.NoteProcessor();


        private void Initialize(Subject businessObjectParent, Note businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;

            InitializeComponent();

            if (IsEditing) {
                Title = "Notiz bearbeiten";
            }
            else {
                Title = "Notiz hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        internal ManageNote(Subject businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        internal ManageNote(Subject businessObjectToEditParent, Note businessObjectToEdit) {
            if (businessObjectToEditParent == null) {
                throw new ArgumentNullException("businessObjectToEditParent");
            }
            if (businessObjectToEdit == null) {
                throw new ArgumentNullException("businessObjectToEdit");
            }

            Initialize(businessObjectToEditParent, businessObjectToEdit);

            MapElementToInterface(businessObjectToEdit);
        }


        public bool IsEditing { get { return businessObjectEditing != null; } }

        public BusinessObjectManagerAction Action { get; private set; }

        #region Control

        private bool Save() {
            if (ValidateInput()) {
                var businessObjectToSave = MapInterfaceToElement();

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

        private void MapElementToInterface(Note businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            tbContent.Text = businessObjectToMap.Content;
        }

        private Note MapInterfaceToElement() {
            if (IsEditing) {
                return new Note(tbName.Text, tbContent.Text, businessObjectParent, businessObjectEditing);
            }
            else {
                return new Note(tbName.Text, tbContent.Text, businessObjectParent);
            }
        }


        private bool ValidateInput() {
            var isNameValid = InputValidation.Validate(tbName);
            var isContentValid = InputValidation.Validate(tbContent);

            if (isNameValid && isContentValid) {
                Note duplicate;
                if (IsEditing) {
                    duplicate = businessObjectProcessor.Get(businessObjectParent).Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper()
                                                                                           && entity.Id != businessObjectEditing.Id);
                }
                else {
                    duplicate = businessObjectProcessor.Get(businessObjectParent).Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper());
                }

                var doesDuplicateExist = duplicate != null;
                if (doesDuplicateExist) {
                    InputValidation.MarkControlAsInvalid(tbName, "Dieser Name wird bereits für eine andere Notiz für dieses Fach verwendet.");
                }
                else {
                    InputValidation.MarkControlAsValid(tbName);
                }

                return !doesDuplicateExist;
            }
            else {
                return false;
            }
        }

        #endregion

        #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            try {
                if (Save()) {
                    Action = BusinessObjectManagerAction.Saved;
                    Close();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            try {
                Action = BusinessObjectManagerAction.Canceled;
                Close();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        #endregion

    }

}
