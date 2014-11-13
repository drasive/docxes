using System;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Provides helper methods for <see cref="DateTime"/> operations.
    /// </summary>
    internal static class DateTimeHelper {

        /// <summary>
        /// Returns the date of the first day of week of the specified date.
        /// </summary>
        /// <param name="date">The date to find the first day of week for.</param>
        /// <returns>The date of the first day of week of the specified date.</returns>
        internal static DateTime GetFirstDayOfWeek(this DateTime date) {
            var startOfWeek = 1; // Monday
            var amountOfDaysBehind = (int)(startOfWeek - date.DayOfWeek);
            DateTime firstDayOfWeek = date.AddDays(amountOfDaysBehind);

            return firstDayOfWeek;
        }

        /// <summary>
        /// Returns the date of the last day of week of the specified date.
        /// </summary>
        /// <param name="date">The date to find the last day of week for.</param>
        /// <returns>The date of the last day of week of the specified date.</returns>
        internal static DateTime GetLastDayOfWeek(this DateTime date) {
            var endOfWeek = 7; // Sunday
            var amountOfDaysAhead = (int)(endOfWeek - date.DayOfWeek);
            DateTime lastDayOfWeek = date.AddDays(amountOfDaysAhead);

            return lastDayOfWeek;
        }

    }

}
