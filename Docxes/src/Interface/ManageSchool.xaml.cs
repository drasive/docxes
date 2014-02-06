using System;
using System.Windows;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchool.xaml"/>
    /// </summary>
    public sealed partial class ManageSchool : Window, IManagementElementManager {

        public ManageSchool() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        public ManageSchool(School elementToEdit)
            : this() {
            if (elementToEdit == null) {
                throw new ArgumentNullException("elementToEdit");
            }

            MapElementToInterface(elementToEdit);
        }


        public ManagementElementManagerAction Action { get; private set; }

        #region Control

        private bool Save() {
            if (ValidateInput()) {
                Data.ManagementElementController<School> controller = new Data.SchoolsController();
                controller.Save(MapInterfaceToElement());
                return true;
            }

            return false;
        }

        private void Cancel() {
            Close();
        }

        #endregion

        #region Interface

        private void MapElementToInterface(School elementToMap) {
            if (elementToMap == null) {
                throw new ArgumentNullException("elementToMap");
            }

            tbName.Text = elementToMap.Name;
            tbComment.Text = elementToMap.Comment;
        }

        private School MapInterfaceToElement() {
            return new School();
        }


        private bool ValidateInput() {
            return InputValidation.ValidateTextBoxInput(tbName);
        }

        #endregion

        #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if (Save()) {
                Action = ManagementElementManagerAction.Saved;
                Cancel();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Action = ManagementElementManagerAction.Canceled;
            Cancel();
        }

        #endregion

    }

}
