using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimitriVranken.MonthViewCalendar {

    /// <summary>
    /// An entry in a calandar.
    /// </summary>
    public class CalendarEvent {

        #region Public Properties

        public string Name { get; private set; }
        public DateTime Date { get; private set; }

        #endregion

        #region Public Constructors

        public CalendarEvent(string name, DateTime date) {
            Name = name;
            Date = date;
        }

        #endregion

    }

}
