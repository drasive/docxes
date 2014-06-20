using System;

namespace VrankenBischof.Docxes {

    // TODO: Document code of Logging
    // TODO: Implement logging

    /// <summary>
    /// This class only exists for the creation of the class diagram. It will probably be replaced later on.
    /// </summary>
    internal static class Logger {

        private static void InitializeLogger() {
            // TODO: ___Test directory
            var DataDirectoryPath = ConfigurationReader.GetDataDirectoryPath();
            JochenScharr.SimpleLog.SetLogDir(DataDirectoryPath);
        }

        internal static void Log(Exception exception) {
            try {
                InitializeLogger();

                JochenScharr.SimpleLog.Log(exception);
            }
            catch {
                // Ignore
            }
        }


    }

}
