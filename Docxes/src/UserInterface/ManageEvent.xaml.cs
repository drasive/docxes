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

            UpdateBusinessObjectParents();
            UpdateBusinessObjectTypes();
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
            Dictionary<string, int> businessObjectTypes = new Dictionary<string, int>();
            foreach (var businessObjectType in Enum.GetValues(typeof(EventType))) {
                businessObjectTypes.Add(VrankenBischof.Docxes.Common.GetEnumDescription((EventType)businessObjectType), (int)businessObjectType);
            }

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
            var name = tbName.Text;
            var place = tbPlace.Text;
            var date = dpDate.SelectedDate.Value;
            var type = (EventType)cbType.SelectedValue;
            var comment = tbComment.Text;
            var subject = (Subject)cbSubject.SelectedItem;

            if (IsEditing) {
                return new Event(name, place, date, type, comment, subject, businessObjectEditing);
            }
            else {
                return new Event(name, place, date, type, comment, subject);
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
