using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// This class contains functionality to validate the input inside a user interface.
    /// </summary>
    public static class InputValidation {

        public static bool ValidateTextBoxInput(TextBox textBox) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            if (String.IsNullOrEmpty(textBox.Text.Trim())) {
                textBox.BorderBrush = Brushes.Red;
                return false;
            }

            textBox.BorderBrush = Brushes.DimGray; // The default border brush of a TextBox
            return true;
        }

    }

}
