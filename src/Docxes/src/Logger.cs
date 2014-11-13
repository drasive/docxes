using System;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Provides functionality to log exceptions.
    /// </summary>
    internal static class Logger {

        private static void InitializeLogger() {
            var DataDirectoryPath = ConfigurationReader.GetDataDirectoryPath();
            JochenScharr.SimpleLog.SetLogDir(DataDirectoryPath);
        }


        /// <summary>
        /// Writes an exception to the log.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
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
