using System;
using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Provides helper methods for working with a user interface.
    /// </summary>
    internal static class Common {

        /// <summary>
        /// Extends the name of the window with the application name.
        /// </summary>
        /// <param name="window">The window to extend the name of.</param>
        internal static void ExtendWindowName(Window window) {
            if (window == null) {
                throw new ArgumentNullException("window");
            }

            window.Title = Application.Current.MainWindow.GetType().Assembly.GetName().Name + " | " + window.Title;
        }

        /// <summary>
        /// Generates a ListBoxItem that can be used as a placeholder.
        /// </summary>
        /// <param name="message">The message to put inside of the placeholder</param>
        internal static ListBoxItem GeneratePlaceholderListBoxItem(String message) {
            var placeholder = new ListBoxItem() {
                Content = message,
                FontSize = 10,
                IsEnabled = false
            };

            return placeholder;
        }

        /// <summary>
        /// Asks the user if he really wants do delete a certain object.
        /// </summary>
        /// <param name="question">The question that will be displayed to the user.</param>
        /// <param name="objectType">The type of the object that will be deleted.</param>
        /// <returns>True if the user confirmed the object deletion; otherwise, false.</returns>
        internal static bool AskForElementDeletion(string question, string objectType) {
            if (String.IsNullOrEmpty(question)) {
                throw new ArgumentNullException("question");
            }
            if (String.IsNullOrEmpty(objectType)) {
                throw new ArgumentNullException("objectType");
            }

            MessageBoxResult result = MessageBox.Show(question + Environment.NewLine +
                                                      "Gelöschte Daten können nicht wiederhergestellt werden.",
                                                      objectType + " wirklich löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            return result == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Shows a generic error message to the user.
        /// </summary>
        internal static void ShowGenericErrorMessage() {
            MessageBox.Show("Beim Ausführen dieser Aktion ist ein Fehler aufgetreten!" + Environment.NewLine +
                            "Versuchen Sie den Vorgang zu wiederholen oder starten Sie die Applikation neu, falls weitere Probleme auftreten sollten.",
                            "Fehler aufgetreten", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

}
