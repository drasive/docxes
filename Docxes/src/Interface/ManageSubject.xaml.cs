using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSubject.xaml"/>
    /// </summary>
    public partial class ManageSubject : Window, IManagementElementManager {

        public ManageSubject() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        public ManageSubject(Subject elementToEdit)
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
                Data.ManagementElementController<Subject> controller = new Data.SubjectsController();
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

        private void MapElementToInterface(Subject elementToMap) {
            if (elementToMap == null) {
                throw new ArgumentNullException("elementToMap");
            }

            tbName.Text = elementToMap.Name;
            // TODO:
            // cbTeachers. = elementToMap.;
        }

        private Subject MapInterfaceToElement() {
            return new Subject();
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
