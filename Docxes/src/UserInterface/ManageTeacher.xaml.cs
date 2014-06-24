using System;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageTeacher.xaml"/>.
    /// </summary>
    internal partial class ManageTeacher : Window, IBusinessObjectManager {

        private School businessObjectParent { get { return ApplicationPropertyManager.Workspace.School; } }
        private Teacher businessObjectEditing;

        private BusinessLogic.TeacherProcessor businessObjectProcessor = new BusinessLogic.TeacherProcessor();


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

        internal ManageTeacher() {
            Initialize(null);
        }

        internal ManageTeacher(Teacher businessObjectToEdit) {
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
            var firstName = tbFirstName.Text;
            var lastName = tbLastName.Text;
            var isMale = cbIsMale.IsChecked.GetValueOrDefault();

            if (IsEditing) {
                return new Teacher(firstName, lastName, isMale, businessObjectParent, businessObjectEditing);
            }
            else {
                return new Teacher(firstName, lastName, isMale, businessObjectParent);
            }
        }


        private bool ValidateInput() {
            var isFirstNameValid = InputValidation.Validate(tbFirstName);
            var isLastNameValid = InputValidation.Validate(tbLastName);

            if (isFirstNameValid && isLastNameValid) {
                School currentSchool = ApplicationPropertyManager.Workspace.School;
                Teacher duplicate;
                if (IsEditing) {
                    duplicate = businessObjectProcessor.Get(currentSchool).Find(entity => entity.FirstName.ToUpper() == tbFirstName.Text.ToUpper()
                                                                                              && entity.LastName.ToUpper() == tbLastName.Text.ToUpper()
                                                                                              && entity.Id != businessObjectEditing.Id);
                }
                else {
                    duplicate = businessObjectProcessor.Get(currentSchool).Find(entity => entity.FirstName.ToUpper() == tbFirstName.Text.ToUpper()
                                                                                              && entity.LastName.ToUpper() == tbLastName.Text.ToUpper());
                }

                var doesDuplicateExist = duplicate != null;
                if (doesDuplicateExist) {
                    var toolTip = "Diese Kombination von Vor- und Nachname wird bereits für einen anderen Lehrer an dieser Schule verwendet.";
                    InputValidation.MarkControlAsInvalid(tbFirstName, toolTip);
                    InputValidation.MarkControlAsInvalid(tbLastName, toolTip);
                }
                else {
                    InputValidation.MarkControlAsValid(tbFirstName);
                    InputValidation.MarkControlAsValid(tbLastName);
                }

                return !doesDuplicateExist;
            }
            else {
                return false;
            }
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
