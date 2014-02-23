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

    /// <summary>
    /// Interaction logic for <see cref="ManageTeacher.xaml"/>
    /// </summary>
    public partial class ManageTeacher : Window/*,  IBusinessObjectManager */{

    //    public ManageTeacher() {
    //        InitializeComponent();

    //        Common.ExtendWindowName(this);
    //    }

    //    public ManageTeacher(Teacher elementToEdit)
    //        : this() {
    //        if (elementToEdit == null) {
    //            throw new ArgumentNullException("elementToEdit");
    //        }

    //        MapElementToInterface(elementToEdit);
    //    }


    //    public BusinessObjectManagerAction Action { get; private set; }

    //    #region Control

    //    private bool Save() {
    //        if (ValidateInput()) {
    //            Data.ManagementObjectDataManager<Teacher> controller = new Data.TeachersDataManager();
    //            controller.Save(MapInterfaceToElement());
    //            return true;
    //        }

    //        return false;
    //    }

    //    private void Cancel() {
    //        Close();
    //    }

    //    #endregion

    //    #region Interface

    //    private void MapElementToInterface(Teacher elementToMap) {
    //        if (elementToMap == null) {
    //            throw new ArgumentNullException("elementToMap");
    //        }

    //        tbFirstName.Text = elementToMap.FirstName;
    //        tbLastName.Text = elementToMap.LastName;
    //        cbIsMale.IsChecked = elementToMap.IsMale;
    //    }

    //    private Teacher MapInterfaceToElement() {
    //        return new Teacher();
    //    }


    //    private bool ValidateInput() {
    //        return InputValidation.ValidateTextBoxInput(tbFirstName) && InputValidation.ValidateTextBoxInput(tbLastName); ;
    //    }

    //    #endregion        

    //    #region Event wiring

        private void btnSave_Click(object sender, RoutedEventArgs e) {
    //        if (Save()) {
    //            Action = BusinessObjectManagerAction.Saved;
    //            Cancel();
    //        }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
    //        Action = BusinessObjectManagerAction.Canceled;
    //        Cancel();
        }

    //    #endregion

    }

}
