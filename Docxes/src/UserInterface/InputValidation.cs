using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace VrankenBischof.Docxes.UserInterface {

    // TODO: Fix empty tooltip

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

            control.Foreground = Brushes.Black;
            control.BorderBrush = Brushes.DimGray; // Pretty similar to the default border brush of a TextBox; 
            control.ToolTip = string.Empty;
        }

        /// <summary>
        /// Marks a control as invalid by setting the border color to red.
        /// </summary>
        /// <param name="control">The control to mark as invalid.</param>
        /// <param name="toolTip">The tooltip to use for the specified control.</param>
        internal static void MarkControlAsInvalid(Control control, string toolTip, bool styleBorderInsteadOfText = false) {
            if (control == null) {
                throw new ArgumentNullException("control");
            }

            if (styleBorderInsteadOfText) {
                control.BorderBrush = Brushes.Red;
            }
            else {
                control.Foreground = Brushes.Red;
            }
            control.ToolTip = toolTip ?? String.Empty;
        }


        /// <summary>
        /// Validates the text entered in a text box.
        /// </summary>
        /// <param name="textBox">The text box to validate the text of.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(TextBox textBox) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            if (String.IsNullOrEmpty(textBox.Text.Trim())) {
                MarkControlAsInvalid(textBox, "Dies ist ein Pflichtfeld. Bitte geben Sie einen Wert ein.", true);
                return false;
            }
            else {
                MarkControlAsValid(textBox);
                return true;
            }
        }

        /// <summary>
        /// Validates the text entered in a text box.
        /// </summary>
        /// <param name="textBox">The text box to validate the text of.</param>
        /// <param name="minimumValue">The minimum numerical, decimal value of the text box text.</param>
        /// <param name="maximumValue">The minimum numerical, decimal value of the text box text.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(TextBox textBox, decimal minimumValue, decimal maximumValue) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }
            if (minimumValue > maximumValue) {
                throw new ArgumentException("\"minimumValue\" is larger than \"maximumValue\"");
            }

            // General validation
            if (Validate(textBox)) {
                // Data type validation
                decimal inputAsDecimal;
                var isDataTypeValid = Decimal.TryParse(Common.EscapeNumber(textBox.Text), out inputAsDecimal);
                if (isDataTypeValid) {
                    InputValidation.MarkControlAsValid(textBox);
                }
                else {
                    InputValidation.MarkControlAsInvalid(textBox, "Dieser Wert ist keine gültige Dezimalzahl. Bitte geben Sie eine Dezimalzahl im Format \"0,##\" ein (z.B.: 4 oder 5,87).");
                    return false;
                }

                // Range validation
                var isValueInValidRange = inputAsDecimal >= minimumValue && inputAsDecimal <= maximumValue;
                if (isValueInValidRange) {
                    InputValidation.MarkControlAsValid(textBox);
                }
                else {
                    InputValidation.MarkControlAsInvalid(textBox, String.Format("Dieser Zahlenwert befindet sich ausserhalb des gültigen Bereiches. Bitte geben Sie eine Dezimalzahl zwischen {0} und {1} ein.", minimumValue, maximumValue));
                    return false;
                }

                // Return
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the text entered in a text box.
        /// </summary>
        /// <param name="textBox">The text box to validate the text of.</param>
        /// <param name="minimumValue">The minimum numerical, integer value of the text box text.</param>
        /// <param name="maximumValue">The minimum numerical, integer value of the text box text.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(TextBox textBox, int minimumValue, int maximumValue) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }
            if (minimumValue > maximumValue) {
                throw new ArgumentException("\"minimumValue\" is larger than \"maximumValue\"");
            }

            // General validation
            if (Validate(textBox)) {
                // Data type validation
                int inputAsInteger = 0;
                var isDataTypeValid = Int32.TryParse(textBox.Text, out inputAsInteger);
                if (isDataTypeValid) {
                    InputValidation.MarkControlAsValid(textBox);
                }
                else {
                    InputValidation.MarkControlAsInvalid(textBox, "Dieser Wert ist keine gültige Ganzzahl. Bitte geben Sie eine Ganzzahl im Format \"0\" ein (z.B.: 4 oder 86).");
                    return false;
                }

                // Range validation
                var isValueInValidRange = inputAsInteger >= minimumValue && inputAsInteger <= maximumValue;
                if (isValueInValidRange) {
                    InputValidation.MarkControlAsValid(textBox);
                }
                else {
                    InputValidation.MarkControlAsInvalid(textBox, String.Format("Dieser Zahlenwert befindet sich ausserhalb des gültigen Bereiches. Bitte geben Sie eine Ganzzahl zwischen {0} und {1} ein.", minimumValue, maximumValue));
                    return false;
                }

                // Return
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the date selected in date picker.
        /// </summary>
        /// <param name="datePicker">The date picker to validate the input of.</param>
        /// <returns>>True if the input is valid; otherwise, false.</returns>
        internal static bool Validate(DatePicker datePicker) {
            if (datePicker == null) {
                throw new ArgumentNullException("datePicker");
            }

            if (datePicker.SelectedDate == null) {
                MarkControlAsInvalid(datePicker, "Dies ist ein Pflichtfeld. Bitte geben Sie einen Wert ein.", true);
                return false;
            }
            else {
                MarkControlAsValid(datePicker);
                return true;
            }
        }

    }

}
