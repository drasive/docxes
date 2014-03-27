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
    /// Interaction logic for <see cref="ManageDocument.xaml"/>
    /// </summary>
    public partial class ManageDocument : Window, IBusinessObjectManager {

        private Document businessObjectEditing;

        private BusinessLogic.BusinessObjectProcessor<Document> businessObjectProcessor = new BusinessLogic.DocumentProcessor();


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

        public ManageDocument() {
            Initialize();
        }

        public ManageDocument(Document businessObjectToEdit) {
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
                if (IsEditing) {
                    businessObjectProcessor.Update(MapInterfaceToElement());
                }
                else {
                    businessObjectProcessor.Create(MapInterfaceToElement());
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

        private void MapElementToInterface(Document businessObjectToMap) {
            if (businessObjectToMap == null) {
                throw new ArgumentNullException("businessObjectToMap");
            }

            // TODO:
            //tbName.Text = businessObjectToMap.Name;
            //cbTeacher.SelectedItem = businessObjectToMap.Teacher;
        }

        private Document MapInterfaceToElement() {
            // TODO:
            //if (IsEditing) {
            //    return new Document(businessObjectEditing, tbName.Text, (Teacher)cbTeacher.SelectedItem);
            //}
            //else {
            //    return new Document(tbName.Text, (Teacher)cbTeacher.SelectedItem);
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
