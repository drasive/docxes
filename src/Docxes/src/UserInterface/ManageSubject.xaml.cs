using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageSubject.xaml"/>.
    /// </summary>
    internal partial class ManageSubject : Window, IBusinessObjectManager {

        private Teacher businessObjectParent;
        private Subject businessObjectEditing;

        private BusinessLogic.TeacherProcessor businessObjectParentProcessor = new BusinessLogic.TeacherProcessor();
        private BusinessLogic.SubjectProcessor businessObjectProcessor = new BusinessLogic.SubjectProcessor();


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
            var name = tbName.Text;
            var teacher = (Teacher)cbTeacher.SelectedItem;

            if (IsEditing) {
                return new Subject(name, teacher, businessObjectEditing);
            }
            else {
                return new Subject(name, teacher);
            }
        }


        private bool ValidateInput() {
            var isNameValid = InputValidation.Validate(tbName);

            if (isNameValid) {
                School currentSchool = ApplicationPropertyManager.Workspace.School;
                Subject duplicate;
                if (IsEditing) {
                    duplicate = businessObjectProcessor.Get(currentSchool).Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper()
                                                                                          && entity.Id != businessObjectEditing.Id);
                }
                else {
                    duplicate = businessObjectProcessor.Get(currentSchool).Find(entity => entity.Name.ToUpper() == tbName.Text.ToUpper());
                }

                var doesDuplicateExist = duplicate != null;
                if (doesDuplicateExist) {
                    InputValidation.MarkControlAsInvalid(tbName, "Dieser Name wird bereits für ein anderes Fach an dieser Schule verwendet.");
                }
                else {
                    InputValidation.MarkControlAsValid(tbName);
                }

                return !doesDuplicateExist;
            }
            else {
                return false;
            }
        }

        #endregion

        #region Event wiring

        private void validatedControl_InputChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            try {
                var control = (Control)sender;

                InputValidation.MarkControlAsValid(control);
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


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
