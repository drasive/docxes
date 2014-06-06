using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Manages the properties that are used throughout the application.
    /// </summary>
    public static class ApplicationPropertyManager {

        private static string workspaceKey = "workspace";


        public static Application Application { get; set; }

        public static Workspace Workspace {
            get {
                return (Workspace)Application.Properties[workspaceKey];
            }
            set {
                Application.Properties[workspaceKey] = value;
            }
        }
    
    }

}
