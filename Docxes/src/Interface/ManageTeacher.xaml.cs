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

        private School businessObjectParent { get { return ApplicationPropertyManager.Workspace.School; } }
        private Teacher businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Teacher, School> businessObjectProcessor = new BusinessLogic.TeacherProcessor();


        private void Initialize(Teacher businessObjectToEdit) {
            InitializeComponent();

            this.businessObjectEditing = businessObjectToEdit;

            if (IsEditing) {
                Title = "Lehrer bearbeiten";
            }
            else {
                Title = "Lehrer hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        public ManageTeacher() {
            Initialize(null);
        }

        public ManageTeacher(Teacher businessObjectToEdit) {
            if (businessObjectToEdit == null) {
                throw new ArgumentNullException("businessObjectToEdit");
            }

            Initialize(businessObjectToEdit);

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
            if (IsEditing) {
                return new Teacher(businessObjectEditing, tbFirstName.Text, tbLastName.Text, cbIsMale.IsChecked.GetValueOrDefault(), businessObjectParent);
            }
            else {
                return new Teacher(tbFirstName.Text, tbLastName.Text, cbIsMale.IsChecked.GetValueOrDefault(), businessObjectParent);
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
