using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// This class contains common functionality, which can be used in the context of a user interface.
    /// </summary>
    public static class Common {

        public static void ExtendWindowName(Window window) {
            if (window == null) {
                throw new ArgumentNullException("window");
            }

            window.Title = Application.Current.MainWindow.GetType().Assembly.GetName().Name + " | " + window.Title;
        }

        public static bool AskForElementDeletion(string question, string objectType) {
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

    }

}
