using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DimitriVranken.MonthViewCalendar {

    /// <summary>
    /// A calendar control that supports events.
    /// </summary>
    public class MonthViewCalendar : Microsoft.Windows.Controls.Calendar, INotifyPropertyChanged {

        #region Events Property

        private const string EventsPropertyName = "Events";

        public static DependencyProperty EventsProperty =
           DependencyProperty.Register
           (
               EventsPropertyName,
               typeof(ObservableCollection<CalendarEvent>),
               typeof(Microsoft.Windows.Controls.Calendar)
           );

        /// <summary>
        /// The list of <see cref="CalendarEvents"/>. This is a dependency property.
        /// </summary>
        public ObservableCollection<CalendarEvent> Events {
            get { return (ObservableCollection<CalendarEvent>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        #endregion

        #region Public Constructors

        static MonthViewCalendar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthViewCalendar), new FrameworkPropertyMetadata(typeof(MonthViewCalendar)));
        }

        public MonthViewCalendar()
            : base() {
            SetValue(EventsProperty, new ObservableCollection<CalendarEvent>());
        }

        #endregion

        #region Public Events

        private void OnDayDoubleClicked(EventArgs eventArgs) {
            var handler = DayDoubleClicked;
            if (handler != null) {
                handler(this, eventArgs);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void DayDoubleClickedDelegate(object sender, EventArgs eventArgs);
        public event DayDoubleClickedDelegate DayDoubleClicked;

        #endregion

        #region Protected Methods

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
            base.OnMouseDoubleClick(e);

            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element.DataContext is DateTime) {
                OnDayDoubleClicked(EventArgs.Empty);
            }
        }

        #endregion

    }

}
