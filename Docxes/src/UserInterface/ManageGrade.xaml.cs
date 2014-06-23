using System;
using System.Collections.Generic;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    // TODO: Interface
    // TODO: Selected stuff

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
            UpdateBusinessObjectWeights();
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

        private void UpdateBusinessObjectWeights() {
            Dictionary<string, int> businessObjectTypes = new Dictionary<string, int>();
            foreach (var businessObjectType in Enum.GetValues(typeof(GradeWeight))) {
                businessObjectTypes.Add(VrankenBischof.Docxes.Common.GetEnumDescription((GradeWeight)businessObjectType), (int)businessObjectType);
            }

            cbWeight.ItemsSource = businessObjectTypes;
            cbWeight.SelectedIndex = 0;
        }


        private void MapElementToInterface(Grade businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbGrade.Text = businessObjectToMap.Value.ToString();
            cbWeight.SelectedItem = businessObjectToMap.Weight;
            cbSubject.SelectedItem = businessObjectToMap.Subject;
            tbComment.Text = businessObjectToMap.Comment;
        }

        private Grade MapInterfaceToElement() {
            if (IsEditing) {
                return new Grade(Decimal.Parse(tbGrade.Text), (GradeWeight)cbWeight.SelectedItem, tbComment.Text, (Subject)cbSubject.SelectedItem);
            }
            else {
                return new Grade(Decimal.Parse(tbGrade.Text), (GradeWeight)cbWeight.SelectedItem, tbComment.Text, (Subject)cbSubject.SelectedItem, businessObjectEditing);
            }
        }


        private bool ValidateInput() {
            // TODO: Outsource

            // Validate presence
            var isGradeFilledIn = InputValidation.Validate(tbGrade);
            if (!isGradeFilledIn) {
                return false;
            }

            // Validate data type
            decimal gradeAsDecimal;
            var isGradeValidDecimal = Decimal.TryParse(tbGrade.Text.Replace('.', ','), out gradeAsDecimal);
            if (isGradeValidDecimal) {
                InputValidation.MarkControlAsValid(tbGrade);
            }
            else {
                InputValidation.MarkControlAsInvalid(tbGrade, "Dieser Wert ist keine gültige Zahl. Bitte geben Sie eine Zahl im Format 0,## ein (z.B.: 5 oder 4,87).");
                return false;
            }

            // Validate range
            var isGradeInValidRange = gradeAsDecimal >= 1 && gradeAsDecimal <= 6;
            if (isGradeInValidRange) {
                InputValidation.MarkControlAsValid(tbGrade);
            }
            else {
                InputValidation.MarkControlAsInvalid(tbGrade, "Dieser Wert entspricht keiner gültigen Note. Bitte geben Sie einen Wert zwischen 1 und 6 ein.");
                return false;
            }

            return true;
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
