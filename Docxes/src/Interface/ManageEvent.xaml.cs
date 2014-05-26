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

    // TODO: Add start and end time to event?

    /// <summary>
    /// Interaction logic for <see cref="ManageEvent.xaml"/>
    /// </summary>
    public partial class ManageEvent : Window, IBusinessObjectManager {

        private Subject businessObjectParent;
        private Event businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Event, Subject> businessObjectProcessor = new BusinessLogic.EventProcessor();


        private void Initialize(Subject businessObjectParent, Event businessObjectToEdit) {
            if (businessObjectParent == null) {
                throw new ArgumentNullException("businessObjectParent");
            }

            this.businessObjectParent = businessObjectParent;
            this.businessObjectEditing = businessObjectToEdit;

            InitializeComponent();

            if (IsEditing) {
                Title = "Ereignis bearbeiten";
            }
            else {
                Title = "Ereignis hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        public ManageEvent(Subject businessObjectToAddParent) {
            if (businessObjectToAddParent == null) {
                throw new ArgumentNullException("businessObjectToAddParent");
            }

            Initialize(businessObjectToAddParent, null);
        }

        public ManageEvent(Subject businessObjectToEditParent, Event businessObjectToEdit) {
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

        private void MapElementToInterface(Event businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            tbName.Text = businessObjectToMap.Name;
            tbPlace.Text = businessObjectToMap.Place;
            dpDate.SelectedDate = businessObjectToMap.Date;
            // ASK: Where to store the types (DB/ app)?
            cbType.SelectedValue = businessObjectToMap.Type;
            tbComment.Text = businessObjectToMap.Comment;
        }

        private Event MapInterfaceToElement() {
            if (IsEditing) {
                return new Event(businessObjectEditing, tbName.Text, tbPlace.Text, dpDate.SelectedDate.Value, (int)cbType.SelectedValue, tbComment.Text, businessObjectParent);
            }
            else {
                return new Event(tbName.Text, tbPlace.Text, dpDate.SelectedDate.Value, (int)cbType.SelectedValue, tbComment.Text, businessObjectParent);
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
