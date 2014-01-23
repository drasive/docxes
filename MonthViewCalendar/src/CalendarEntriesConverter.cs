using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace DimitriVranken.MonthViewCalendar {

    /// <summary>
    /// Gets the all of the <see cref="CalendarEntry" /> for the specified date.
    /// </summary>
    [ValueConversion(typeof(ObservableCollection<CalendarEvent>), typeof(ObservableCollection<CalendarEvent>))]
    public class CalendarEntriesConverter : IMultiValueConverter {

        #region Pubic Methods

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            DateTime date = (DateTime)values[1];

            ObservableCollection<CalendarEvent> calendarEvents = new ObservableCollection<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in (ObservableCollection<CalendarEvent>)values[0]) {
                if (calendarEvent.Date.Date == date) {
                    calendarEvents.Add(calendarEvent);
                }
            }

            return calendarEvents;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion

    }

}
