using System;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents the exception that is thrown when an invalid configuration setting is read.
    /// </summary>
    internal sealed class InvalidConfigurationSettingException : Exception {

        private string _key;

        internal string Key {
            get { return _key; }
        }


        /// <summary>
        /// Creates a new instance of the class <see cref="EventInvalidConfigurationSettingException"/>.
        /// </summary>
        /// <param name="key">The key of the invalid configuration setting.</param>
        internal InvalidConfigurationSettingException(string key)
            : base("The configuration setting with the key \"" + key + "\" is invalid.") {
                var a = new InvalidOperationException();
            _key = key;
        }

    }

}
