using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchool.xaml"/>.
    /// </summary>
    internal sealed partial class ManageSchool : Window, IBusinessObjectManager {

        private School businessObjectEditing;

        private BusinessLogic.SchoolProcessor businessObjectProcessor = new BusinessLogic.SchoolProcessor();


        private void Initialize() {
            InitializeComponent();

            if (IsEditing) {
                Title = "Schule bearbeiten";
            }
            else {
                Title = "Schule hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        internal ManageSchool() {
            Initialize();
        }

        internal ManageSchool(School businessObjectToEdit) {
            if (businessObjectToEdit == null) {
                throw new ArgumentNullException("businessObjectToEdit");
            }

            this.businessObjectEditing = businessObjectToEdit;

            Initialize();

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

        private void MapElementToInterface(School businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            tbComment.Text = businessObjectToMap.Comment;
        }

        private School MapInterfaceToElement() {
            if (IsEditing) {
                return new School(tbName.Text, tbComment.Text, businessObjectEditing);
            }
            else {
                return new School(tbName.Text, tbComment.Text);
            }
        }


        private bool ValidateInput() {
            var isNameValid = InputValidation.Validate(tbName);

            if (isNameValid) {
                School duplicate;
                if (IsEditing) {
                    duplicate = businessObjectProcessor.Get().Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper()
                                                                                 && entity.Id != businessObjectEditing.Id);
                }
                else {
                    duplicate = businessObjectProcessor.Get().Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper());
                }

                var doesDuplicateExist = duplicate != null;
                if (doesDuplicateExist) {
                    InputValidation.MarkControlAsInvalid(tbName, "Dieser Name wird bereits für eine andere Schule verwendet.");
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
