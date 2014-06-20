using System;
using System.Configuration;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Provides functionality to read configuration files.
    /// </summary>
    internal sealed class ConfigurationReader {

        #region "General"

        /// <summary>
        /// Gets the value of the DataDirectoryPath setting in the application configuration.
        /// </summary>
        /// <returns>The value of the DataDirectoryPath setting in the application configuration.</returns>
        internal static string GetDataDirectoryPath() {
            return GetPathValue("DataDirectoryPath");
        }

        #endregion


        #region "Helper Methods"

        private static string GetConnectionStringValue(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[key];
            if (ConnectionStringSettings == null) {
                throw new InvalidConfigurationSettingException(key);
            }

            string ConnectionString = ConnectionStringSettings.ConnectionString;
            if (ConnectionString.Length < 5) {
                throw new InvalidConfigurationSettingException(key);
            }

            return ConnectionString;
        }


        private static string GetStringValue(string key, int minimumLength = 0, int maximumLength = int.MaxValue) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            string AppSetting = ConfigurationManager.AppSettings.Get(key);
            if (AppSetting == null) {
                throw new InvalidConfigurationSettingException(key);
            }
            if (AppSetting.Length < minimumLength) {
                throw new InvalidConfigurationSettingException(key);
            }
            if (AppSetting.Length > maximumLength) {
                throw new InvalidConfigurationSettingException(key);
            }

            return AppSetting;
        }

        private static bool GetBooleanValue(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            return Convert.ToBoolean(GetStringValue(key, 4, 5));
        }


        private static string GetPathValue(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            return GetStringValue(key, 3, 255);
        }

        private static string GetHostValue(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            return GetStringValue(key, 3, 1024);
        }

        private static string GetEMailAddressValue(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            return GetStringValue(key, 5, 1024);
        }

        #endregion

    }

}
