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
    public partial class ManageSubject : Window, IBusinessObjectManager {

        private Teacher businessObjectParent;
        private Subject businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Subject> businessObjectProcessor = new BusinessLogic.SubjectProcessor();


        private void Initialize(Teacher businessObjectParent, Subject businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            InitializeComponent();

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;

            if (IsEditing) {
                Title = "Fach bearbeiten";
            }
            else {
                Title = "Fach hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        public ManageSubject(Teacher businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        public ManageSubject(Teacher businessObjectToEditParent, Subject businessObjectToEdit) {
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

        private void MapElementToInterface(Subject businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Subject MapInterfaceToElement() {
            if (IsEditing) {
                return new Subject(businessObjectEditing, tbName.Text, (Teacher)cbTeacher.SelectedItem);
            }
            else {
                return new Subject(tbName.Text, (Teacher)cbTeacher.SelectedItem);
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
