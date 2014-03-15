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
    /// Interaction logic for <see cref="ManageTeacher.xaml"/>
    /// </summary>
    public partial class ManageTeacher : Window, IBusinessObjectManager {

        private Teacher businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Teacher> businessObjectProcessor = new BusinessLogic.TeacherProcessor();


        private void Initialize() {
            InitializeComponent();

            if (IsEditing) {
                Title = "Lehrer bearbeiten";
            }
            else {
                Title = "Lehrer hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        public ManageTeacher() {
            Initialize();
        }

        public ManageTeacher(Teacher businessObjectToEdit) {
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

        private void MapElementToInterface(Teacher businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbFirstName.Text = businessObjectToMap.FirstName;
            tbLastName.Text = businessObjectToMap.LastName;
            cbIsMale.IsChecked = businessObjectToMap.IsMale;
        }

        private Teacher MapInterfaceToElement() {
            // TODO: Use real school
            if (IsEditing) {
                return new Teacher(businessObjectEditing, tbFirstName.Text, tbLastName.Text, cbIsMale.IsChecked.GetValueOrDefault(), new School());
            }
            else {
                return new Teacher(tbFirstName.Text, tbLastName.Text, cbIsMale.IsChecked.GetValueOrDefault(), new School());
            }
        }


        private bool ValidateInput() {
            // ASK: If the bitwise operator is an appropriate solution to the short-circuit problem
            return InputValidation.ValidateInput(tbFirstName) & InputValidation.ValidateInput(tbLastName);
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
