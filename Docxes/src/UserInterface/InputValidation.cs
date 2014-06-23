using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Provides functionality to validate user input.
    /// </summary>
    internal static class InputValidation {

        /// <summary>
        /// Marks a control as valid by restoring the default border color and removing the tooltip.
        /// </summary>
        /// <param name="control">The control to mark as valid.</param>
        internal static void MarkControlAsValid(Control control) {
            if (control == null) {
                throw new ArgumentNullException("control");
            }

            control.BorderBrush = Brushes.DimGray; // Pretty similar to the default border brush of a TextBox
            control.ToolTip = string.Empty;
        }

        /// <summary>
        /// Marks a control as invalid by setting the border color to red.
        /// </summary>
        /// <param name="control">The control to mark as invalid.</param>
        /// <param name="toolTip">The tooltip to use for the specified control.</param>
        internal static void MarkControlAsInvalid(Control control, string toolTip) {
            if (control == null) {
                throw new ArgumentNullException("control");
            }

            control.BorderBrush = Brushes.Red;
            control.ToolTip = toolTip ?? String.Empty;
        }


        /// <summary>
        /// Validates the input inside of a text box.
        /// </summary>
        /// <param name="textBox">The text box to validate the input of.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(TextBox textBox) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            if (String.IsNullOrEmpty(textBox.Text.Trim())) {
                MarkControlAsInvalid(textBox, "Dies ist ein Pflichtfeld");
                return false;
            }
            else {
                MarkControlAsValid(textBox);
                return true;
            }
        }

        /// <summary>
        /// Validates the input inside of a date picker.
        /// </summary>
        /// <param name="datePicker">The date picker to validate the input of.</param>
        /// <returns>>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(DatePicker datePicker) {
            if (datePicker == null) {
                throw new ArgumentNullException("datePicker");
            }

            if (datePicker.SelectedDate == null) {
                MarkControlAsInvalid(datePicker, "Dies ist ein Pflichtfeld");
                return false;
            }
            else {
                MarkControlAsValid(datePicker);
                return true;
            }
        }

    }

}
