using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageGrades.xaml"/>.
    /// </summary>
    internal sealed partial class ManageGrades : Window {

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.GradeProcessor businessObjectProcessor = new BusinessLogic.GradeProcessor();


        internal ManageGrades() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private Subject SelectedBusinessObjectParent { get { return (Subject)cbSubjects.SelectedItem; } }

        private void UpdateBusinessObjectParents() {
            IEnumerable<Subject> businessObjectParents = businessObjectParentProcessor.Get(ApplicationPropertyManager.Workspace.School);

            cbSubjects.ItemsSource = businessObjectParents;
            if (ApplicationPropertyManager.Workspace.Subject != null) {
                cbSubjects.SelectedValue = ApplicationPropertyManager.Workspace.Subject.Id;
            }
            else {
                cbSubjects.SelectedIndex = 0;
            }
        }


        private Grade SelectedBusinessObject { get { return (Grade)lvGrades.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Grade> businessObjects = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            lvGrades.ItemsSource = businessObjects;
        }


        private void UpdateSubjectAverage() {
            var subjectGrades = businessObjectProcessor.Get(SelectedBusinessObjectParent);
            if (subjectGrades.Count > 0) {
                tblSubjectAverage.Text = Math.Round(businessObjectProcessor.CalculateAverageGrade(subjectGrades), 2).ToString("0.00");
            }
            else {
                tblSubjectAverage.Text = "-";
            }
        }

        private void UpdateOverallAverage() {
            var schoolGrades = businessObjectProcessor.Get(ApplicationPropertyManager.Workspace.School);
            if (schoolGrades.Count > 0) {
                tblOverallAverage.Text = Math.Round(businessObjectProcessor.CalculateAverageGrade(schoolGrades), 2).ToString("0.00");
            }
            else {
                tblOverallAverage.Text = "-";
            }
        }


        private void UpdateRequiredGrade() {
            if (tbDesiredAverage.Text.Length == 0) {
                InputValidation.MarkControlAsValid(tbDesiredAverage);
                tblRequiredGrade.Text = "-";
                tblRequiredGrade.ToolTip = String.Empty;

                return;
            }

            // Validate desired grade
            var isDesiredGradeValid = InputValidation.Validate(tbDesiredAverage, 1M, 6M);

            if (!isDesiredGradeValid) {
                tblRequiredGrade.Text = "-";
                tblRequiredGrade.ToolTip = String.Empty;

                return;
            }

            // Calculate required grade
            var subjectGrades = businessObjectProcessor.Get(SelectedBusinessObjectParent);

            if (subjectGrades.Count > 0) {
                var requiredGrade = businessObjectProcessor.CalculateRequiredGrade(subjectGrades, Decimal.Parse(Common.EscapeNumber(tbDesiredAverage.Text)));

                if (requiredGrade != null) {
                    tblRequiredGrade.Text = Math.Round(requiredGrade.Value, 2).ToString("0.00");
                    tblRequiredGrade.ToolTip = String.Empty;
                }
                else {
                    tblRequiredGrade.Text = "-";
                    tblRequiredGrade.ToolTip = "Die gewünschte Note kann mit nur einer zusätzlichen Note nicht erreicht werden!";
                }
            }
            else {
                tblRequiredGrade.Text = "-";
                tblRequiredGrade.ToolTip = String.Empty;
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageGrade(SelectedBusinessObjectParent) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageGrade(SelectedBusinessObjectParent, SelectedBusinessObject) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie diese Note wirklich löschen?", "Note")) {
                businessObjectProcessor.Delete(SelectedBusinessObject);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
            }
        }

        #endregion

        #region Event wiring

        private void wManageGrades_Loaded(object sender, RoutedEventArgs e) {
            try {
                UpdateBusinessObjectParents();

                UpdateBusinessObjects();
                UpdateSubjectAverage();
                UpdateOverallAverage();

                UpdateControlsAvailability();
                UpdateRequiredGrade();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void wManageGrades_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            try {
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H) {
                    if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                        UpdateBusinessObjects();
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B
                         && SelectedBusinessObject != null) {
                    if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                        UpdateBusinessObjects();
                    }
                }
                else if (e.Key == Key.Delete
                         && SelectedBusinessObject != null) {
                    if (CheckForElementDeletion()) {
                        UpdateBusinessObjects();
                    }
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void cbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateBusinessObjects();

                UpdateSubjectAverage();
                UpdateRequiredGrade();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void tbDesiredAverage_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                UpdateRequiredGrade();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateBusinessObjects();

                    UpdateSubjectAverage();
                    UpdateOverallAverage();

                    UpdateRequiredGrade();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateBusinessObjects();

                    UpdateSubjectAverage();
                    UpdateOverallAverage();

                    UpdateRequiredGrade();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                if (CheckForElementDeletion()) {
                    UpdateBusinessObjects();

                    UpdateSubjectAverage();
                    UpdateOverallAverage();

                    UpdateRequiredGrade();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        #endregion

    }

}
