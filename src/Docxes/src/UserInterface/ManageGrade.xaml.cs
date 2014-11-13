using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageGrade.xaml"/>.
    /// </summary>
    internal partial class ManageGrade : Window, IBusinessObjectManager {

        private Subject businessObjectParent;
        private Grade businessObjectEditing;

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.GradeProcessor businessObjectProcessor = new BusinessLogic.GradeProcessor();


        private void Initialize(Subject businessObjectParent, Grade businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;

            InitializeComponent();

            if (IsEditing) {
                Title = "Note bearbeiten";
            }
            else {
                Title = "Note hinzufügen";
            }
            Common.ExtendWindowName(this);

            UpdateBusinessObjectParents();
        }

        internal ManageGrade(Subject businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        internal ManageGrade(Subject businessObjectToEditParent, Grade businessObjectToEdit) {
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
            IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get(ApplicationPropertyManager.Workspace.School);

            cbSubject.ItemsSource = businessObjectParents;
            if (businessObjectParent != null) {
                cbSubject.SelectedValue = businessObjectParent.Id;
            }
            else {
                cbSubject.SelectedIndex = 0;
            }
        }


        private void MapElementToInterface(Grade businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbGrade.Text = businessObjectToMap.Value.ToString();
            tbWeight.Text = businessObjectToMap.Weight.ToString();
            cbSubject.SelectedItem = businessObjectToMap.Subject;
            tbComment.Text = businessObjectToMap.Comment;
        }

        private Grade MapInterfaceToElement() {
            var gradeAsDecimal = Common.ParseDecimal(tbGrade.Text);
            var weightAsInteger = Int32.Parse(tbWeight.Text);
            var comment = tbComment.Text;
            var subject = (Subject)cbSubject.SelectedItem;

            if (IsEditing) {
                return new Grade(gradeAsDecimal, weightAsInteger, comment, subject, businessObjectEditing);
            }
            else {
                return new Grade(gradeAsDecimal, weightAsInteger, comment, subject);
            }
        }


        private bool ValidateInput() {
            var isGradeValid = InputValidation.Validate(tbGrade, 1M, 6M);
            var isWeightValid = InputValidation.Validate(tbWeight, 1, 100);

            return isGradeValid && isWeightValid;
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
