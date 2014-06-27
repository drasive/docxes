using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Manages the properties that are used throughout the application.
    /// </summary>
    internal static class ApplicationPropertyManager {

        private static string workspaceKey = "workspace";


        internal static Application Application { get; set; }

        internal static Workspace Workspace {
            get {
                return (Workspace)Application.Properties[workspaceKey];
            }
            set {
                Application.Properties[workspaceKey] = value;
            }
        }
    
    }

}
