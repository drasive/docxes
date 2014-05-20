using System;
using System.Windows;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchool.xaml"/>
    /// </summary>
    public sealed partial class ManageSchool : Window, IBusinessObjectManager {

        private School businessObjectEditing;

        // TODO:_
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

        public ManageSchool() {
            Initialize();
        }

        public ManageSchool(School businessObjectToEdit) {
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
                if (IsEditing) {
                    businessObjectProcessor.Update(MapInterfaceToElement());
                }
                else {
                    businessObjectProcessor.Create(MapInterfaceToElement());
                }
                return true;
            }

            return false;
        }

        private void Cancel() {
            Close();
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
                return new School(businessObjectEditing, tbName.Text, tbComment.Text);
            }
            else {
                return new School(tbName.Text, tbComment.Text);
            }
        }


        private bool ValidateInput() {
            return InputValidation.ValidateInput(tbName);
        }

        #endregion

        #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if (Save()) {
                Action = BusinessObjectManagerAction.Saved;
                Cancel();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Action = BusinessObjectManagerAction.Canceled;
            Cancel();
        }

        #endregion

    }

}
