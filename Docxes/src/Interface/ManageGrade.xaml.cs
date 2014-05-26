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
    /// Interaction logic for <see cref="ManageGrade.xaml"/>
    /// </summary>
    public partial class ManageGrade : Window, IBusinessObjectManager {

        private Grade businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Grade, Subject> businessObjectProcessor = new BusinessLogic.GradeProcessor();


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

        public ManageGrade() {
            Initialize();
        }

        public ManageGrade(Grade businessObjectToEdit) {
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

        #endregion

        #region Interface

        private void MapElementToInterface(Grade businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            // TODO:
            //tbName.Text = businessObjectToMap.Name;
            //cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Grade MapInterfaceToElement() {
            // TODO:
            //if (IsEditing) {
            //    return new Grade(businessObjectEditing, tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}
            //else {
            //    return new Grade(tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}

            return null;
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
