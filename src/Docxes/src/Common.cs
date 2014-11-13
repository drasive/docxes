using System;
using System.ComponentModel;
using System.Reflection;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Provides general helper methods.
    /// </summary>
    internal static class Common {

        /// <summary>
        /// Gets the description of an enum value.
        /// </summary>
        /// <param name="value">The enum value to get the description for.</param>
        /// <returns>The description of the specified enum value or the value itself as a string, if no description is available.</returns>
        internal static string GetEnumDescription(Enum value) {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null &&
                attributes.Length > 0) {
                return attributes[0].Description;
            }
            else {
                return value.ToString();
            }
        }

    }

}
