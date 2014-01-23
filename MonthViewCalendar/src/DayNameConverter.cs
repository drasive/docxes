using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace DimitriVranken.MonthViewCalendar {

    /// <summary>
    /// Converts the specified short day name to its normal counterpart.
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class DayNameConverter : IValueConverter {

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTimeFormatInfo dateTimeFormat = GetCurrentDateFormat();
            string[] shortestDayNames = dateTimeFormat.ShortestDayNames;
            string[] dayNames = dateTimeFormat.DayNames;

            for (int i = 0; i < shortestDayNames.Count(); i++) {
                if (shortestDayNames[i] == value.ToString()) {
                    return dayNames[i];
                }
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }


        private static DateTimeFormatInfo GetCurrentDateFormat() {
            if (CultureInfo.CurrentCulture.Calendar is GregorianCalendar) {
                return CultureInfo.CurrentCulture.DateTimeFormat;
            }

            foreach (Calendar calendar in CultureInfo.CurrentCulture.OptionalCalendars) {
                if (calendar is GregorianCalendar) {
                    DateTimeFormatInfo dateTimeFormatInfoCurrentCulture = new CultureInfo(CultureInfo.CurrentCulture.Name).DateTimeFormat;
                    dateTimeFormatInfoCurrentCulture.Calendar = calendar;
                    return dateTimeFormatInfoCurrentCulture;
                }
            }

            DateTimeFormatInfo dateTimeFormatInfoInvariantCulture = new CultureInfo(CultureInfo.InvariantCulture.Name).DateTimeFormat;
            dateTimeFormatInfoInvariantCulture.Calendar = new GregorianCalendar();
            return dateTimeFormatInfoInvariantCulture;
        }

    }

}
