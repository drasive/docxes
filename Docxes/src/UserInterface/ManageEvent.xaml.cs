using System;
using System.Collections.Generic;
using System.Windows;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageEvent.xaml"/>.
    /// </summary>
    internal partial class ManageEvent : Window, IBusinessObjectManager {

        private Subject businessObjectParent;
        private Event businessObjectEditing;

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.EventProcessor businessObjectProcessor = new BusinessLogic.EventProcessor();


        private void Initialize(Subject businessObjectParent, Event businessObjectToEdit) {
            InitializeComponent();

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;

            if (IsEditing) {
                Title = "Ereignis bearbeiten";
            }
            else {
                Title = "Ereignis hinzufügen";
            }
            Common.ExtendWindowName(this);

            UpdateBusinessObjectTypes();
            UpdateBusinessObjectParents();
        }

        internal ManageEvent(DateTime date) {
            Initialize(null, null);

            dpDate.SelectedDate = date;
        }

        internal ManageEvent(Subject businessObjectToEditParent, Event businessObjectToEdit) {
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
            cbSubject.SelectedIndex = 0;
        }

        private void UpdateBusinessObjectTypes() {
            IEnumerable<EventType> businessObjectTypes = businessObjectProcessor.GetTypes();

            cbType.ItemsSource = businessObjectTypes;
            cbType.SelectedIndex = 0;
        }


        private void MapElementToInterface(Event businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            cbSubject.SelectedValue = businessObjectParent.Id;
            tbPlace.Text = businessObjectToMap.Place;
            dpDate.SelectedDate = businessObjectToMap.Date;
            cbType.SelectedIndex = (int)businessObjectEditing.Type;
            tbComment.Text = businessObjectToMap.Comment;
        }

        private Event MapInterfaceToElement() {
            if (IsEditing) {
                return new Event(tbName.Text, tbPlace.Text, dpDate.SelectedDate.Value, (int)cbType.SelectedValue, tbComment.Text, (Subject)cbSubject.SelectedItem, businessObjectEditing);
            }
            else {
                return new Event(tbName.Text, tbPlace.Text, dpDate.SelectedDate.Value, (int)cbType.SelectedValue, tbComment.Text, (Subject)cbSubject.SelectedItem);
            }
        }


        private bool ValidateInput() {
            var isNameValid = InputValidation.Validate(tbName);
            var isDateValid = InputValidation.Validate(dpDate);

            return isNameValid && isDateValid;
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
