using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    // TODO: Interface

    /// <summary>
    /// Interaction logic for <see cref="ManageGrade.xaml"/>.
    /// </summary>
    internal partial class ManageGrade : Window, IBusinessObjectManager {

        private Grade businessObjectEditing;

        private BusinessLogic.GradeProcessor businessObjectProcessor = new BusinessLogic.GradeProcessor();


        private void Initialize() {
            InitializeComponent();

            if (IsEditing) {
                Title = "Fach bearbeiten";
            }
            else {
                Title = "Fach hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        internal ManageGrade() {
            Initialize();
        }

        internal ManageGrade(Grade businessObjectToEdit) {
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

        private void MapElementToInterface(Grade businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            // TODO:
            //tbName.Text = businessObjectToMap.Name;
            //cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Grade MapInterfaceToElement() {
            // TODO:
            //if (IsEditing) {
            //    return new Grade(businessObjectEditing, tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}
            //else {
            //    return new Grade(tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}

            return null;
        }


        private bool ValidateInput() {
            return InputValidation.Validate(tbName);
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
