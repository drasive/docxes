using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchool.xaml"/>.
    /// </summary>
    internal sealed partial class ManageSchool : Window, IBusinessObjectManager {

        private School businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<School> businessObjectProcessor = new BusinessLogic.SchoolProcessor();


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
            var isNameValid = InputValidation.ValidateInput(tbName);

            return isNameValid;
        }

        #endregion

        #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if (Save()) {
                Action = BusinessObjectManagerAction.Saved;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Action = BusinessObjectManagerAction.Canceled;
            Close();
        }

        #endregion

    }

}
