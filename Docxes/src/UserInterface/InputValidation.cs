using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Provides functionality to validate user input.
    /// </summary>
    internal static class InputValidation {

        /// <summary>
        /// Validates the input inside of a text box.
        /// </summary>
        /// <param name="textBox">The text box to validate the input of.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        internal static bool ValidateInput(TextBox textBox) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            if (String.IsNullOrEmpty(textBox.Text.Trim())) {
                textBox.BorderBrush = Brushes.Red;
                textBox.ToolTip = "Dies ist ein Pflichtfeld";
                return false;
            }

            // TODO: Check for duplicate entity?

            textBox.BorderBrush = Brushes.DimGray; // The default border brush of a TextBox
            textBox.ToolTip = string.Empty;
            return true;
        }

        /// <summary>
        /// Validates the input inside of a date picker.
        /// </summary>
        /// <param name="datePicker">The date picker to validate the input of.</param>
        /// <returns>>True if the input is valid; otherwise, false.</returns>
        internal static bool ValidateInput(DatePicker datePicker) {
            if (datePicker == null) {
                throw new ArgumentNullException("datePicker");
            }

            if (datePicker.SelectedDate == null) {
                datePicker.BorderBrush = Brushes.Red;
                datePicker.ToolTip = "Dies ist ein Pflichtfeld";
                return false;
            }

            // TODO: _
            datePicker.BorderBrush = Brushes.DimGray; // The default border brush of a TextBox
            datePicker.ToolTip = string.Empty;
            return true;
        }

    }

}
