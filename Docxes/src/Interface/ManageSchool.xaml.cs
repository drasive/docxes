using System.Windows;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSchool.xaml"/>
    /// </summary>
    public partial class ManageSchool : Window {

        public ManageSchool() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        public ManageSchool(School objectToEdit)
            : this() {
            MapObjectToInterface(objectToEdit);
        }

        #region Control

        private bool Save() {
            if (ValidateInput()) {
                Data.ManagementObjectController<School> controller = new Data.SchoolsController();
                controller.Save(MapInterfaceToObject());
                return true;
            }

            return false;
        }

        #endregion

        #region Interface

        protected void MapObjectToInterface(School objectToMap) {
            tbName.Text = objectToMap.Name;
            tbComment.Text = objectToMap.Comment;
        }

        protected School MapInterfaceToObject() {
            return new School();
        }


        protected bool ValidateInput() {
            return InputValidation.ValidateTextBoxInput(tbName);
        }

        #endregion

        #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            if (Save()) {
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        #endregion

    }

}
