using System;
using System.Collections.Generic;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSubject.xaml"/>.
    /// </summary>
    internal partial class ManageSubject : Window, IBusinessObjectManager {

        private Teacher businessObjectParent;
        private Subject businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Teacher, School> businessObjectParentProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> businessObjectProcessor = new BusinessLogic.SubjectProcessor();


        private void Initialize(Teacher businessObjectParent, Subject businessObjectToEdit) {
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

            UpdateBusinessObjectParents();
        }

        internal ManageSubject() {
            Initialize(null, null);
        }

        internal ManageSubject(Teacher businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        internal ManageSubject(Teacher businessObjectToEditParent, Subject businessObjectToEdit) {
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

        private void UpdateBusinessObjectParents() {
            IEnumerable<Teacher> businessObjectParents = businessObjectParentProcessor.Get(ApplicationPropertyManager.Workspace.School);

            cbTeacher.ItemsSource = businessObjectParents;
            if (businessObjectParent != null) {
                cbTeacher.SelectedValue = businessObjectParent.Id;
            }
            else {
                cbTeacher.SelectedIndex = 0;
            }
        }


        private void MapElementToInterface(Subject businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Subject MapInterfaceToElement() {
            if (IsEditing) {
                return new Subject(tbName.Text, (Teacher)cbTeacher.SelectedItem, businessObjectEditing);
            }
            else {
                return new Subject(tbName.Text, (Teacher)cbTeacher.SelectedItem);
            }
        }


        private bool ValidateInput() {
            var isNameValid = InputValidation.Validate(tbName);

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
