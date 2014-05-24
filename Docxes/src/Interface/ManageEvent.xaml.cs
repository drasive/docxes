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

    // TODO: Interface

    /// <summary>
    /// Interaction logic for <see cref="ManageEvent.xaml"/>
    /// </summary>
    public partial class ManageEvent : Window, IBusinessObjectManager {

        private Event businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Event, Subject> businessObjectProcessor = new BusinessLogic.EventProcessor();


        private void Initialize() {
            InitializeComponent();

            if (IsEditing) {
                Title = "Fach bearbeiten";
            }
            else {
                Title = "Fach hinzufügen";
            }
            Common.ExtendWindowName(this);
        }

        public ManageEvent() {
            Initialize();
        }

        public ManageEvent(Event businessObjectToEdit) {
            if (businessObjectToEdit == null) {
                throw new ArgumentNullException("businessObjectToEdit");
            }

            this.businessObjectEditing = businessObjectToEdit;
            Initialize();

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

        private void MapElementToInterface(Event businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            // TODO:
            //tbName.Text = businessObjectToMap.Name;
            //cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Event MapInterfaceToElement() {
            // TODO:
            //if (IsEditing) {
            //    return new Event(businessObjectEditing, tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}
            //else {
            //    return new Event(tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}

            return new Event();
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
