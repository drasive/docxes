using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Interaction logic for <see cref="ManageEvents.xaml"/>.
    /// </summary>
    internal sealed partial class ManageEvents : Window {

        private DateTime _lastDisplayDate;

        private BusinessLogic.SubjectProcessor businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.EventProcessor businessObjectProcessor = new BusinessLogic.EventProcessor();


        internal ManageEvents() {
            InitializeComponent();

            Common.ExtendWindowName(this);
        }

        #region Interface

        private DateTime SelectedDate { get { return cEvents.SelectedDate.Value; } }

        private void UpdateCalendar() {
            // Make sure a date is selected
            if (cEvents.SelectedDate == null) {
                cEvents.SelectedDate = DateTime.Today;
            }

            // Reset highlighted days
            cEvents.HighlightedDateText = new string[31];

            // Update highlighted days
            var monthStart = new DateTime(cEvents.DisplayDate.Year, cEvents.DisplayDate.Month, 1);
            var monthEnd = new DateTime(cEvents.DisplayDate.Year, cEvents.DisplayDate.Month, DateTime.DaysInMonth(monthStart.Year, monthStart.Month));
            List<Event> eventsInMonth = businessObjectProcessor.Get(ApplicationPropertyManager.Workspace.School, monthStart, monthEnd);

            if (eventsInMonth.Count > 0) {
                var currentDate = monthStart;

                while (currentDate.Date < monthEnd.Date) {
                    List<Event> eventsThisDay = eventsInMonth.Where(@event => @event.Date.Date == currentDate.Date).ToList();

                    if (eventsThisDay.Count > 0) {
                        // Mark day as highlighted
                        var highlightedDateText = eventsThisDay.Count.ToString() + " " + ((eventsThisDay.Count > 1) ? "Ereignisse" : "Ereignis");
                        cEvents.HighlightedDateText[currentDate.Day - 1] = highlightedDateText;
                    }

                    currentDate = currentDate.AddDays(1);
                }
            }

            // Refresh calendar
            cEvents.Refresh();
        }

        private void SetCalendarToToday() {
            DateTime today = DateTime.Today;

            cEvents.DisplayMode = CalendarMode.Month;
            cEvents.DisplayDate = today;
            cEvents.SelectedDate = today;
        }


        private Subject SelectedBusinessObjectParent { get { return SelectedBusinessObject.Subject; } }

        private Event SelectedBusinessObject { get { return (Event)lvEvents.SelectedItem; } }

        private void UpdateBusinessObjects() {
            List<Event> businessObjects = businessObjectProcessor.Get(ApplicationPropertyManager.Workspace.School, SelectedDate);

            // Update title
            tblEvents.Text = "Ereignisse am " + SelectedDate.ToShortDateString() + " (" + businessObjects.Count + "):";

            // Update list
            lvEvents.ItemsSource = businessObjects;
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageEvent(SelectedDate) { Owner = this };
            addBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)addBusinessObjectManager).Action;
        }

        private BusinessObjectManagerAction OpenEditBusinessObjectManager() {
            Window editBusinessObjectManager = new ManageEvent(SelectedBusinessObjectParent, SelectedBusinessObject) { Owner = this };
            editBusinessObjectManager.ShowDialog();
            return ((IBusinessObjectManager)editBusinessObjectManager).Action;
        }

        private bool CheckForElementDeletion() {
            if (Common.AskForElementDeletion("Wollen Sie dieses Ereignis wirklich löschen?", "Ereignis")) {
                businessObjectProcessor.Delete((Event)lvEvents.SelectedItem);
                return true;
            }

            return false;
        }

        private void UpdateControlsAvailability() {
            foreach (Button button in new Button[] { btnEdit, btnDelete }) {
                button.IsEnabled = SelectedBusinessObject != null;
            }
        }

        #endregion

        #region Event wiring

        private void wManageEvents_Loaded(object sender, RoutedEventArgs e) {
            try {
                UpdateCalendar();
                UpdateBusinessObjects();
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void cEvents_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateBusinessObjects();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void cEvents_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e) {
            try {
                bool isCalendarRefreshRequired = cEvents.DisplayDate != null && cEvents.DisplayDate != DateTime.MinValue && cEvents.DisplayDate.Date != _lastDisplayDate.Date;
                if (isCalendarRefreshRequired) {
                    _lastDisplayDate = cEvents.DisplayDate;

                    UpdateCalendar();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnToday_Click(object sender, RoutedEventArgs e) {
            try {
                SetCalendarToToday();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e) {
            try {
                base.OnPreviewMouseUp(e);

                // Required to remove the necessity for an additional click to loose focus of a calendar
                if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem) {
                    Mouse.Capture(null);
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                UpdateControlsAvailability();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateCalendar();
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            try {
                if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                    UpdateCalendar();
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            try {
                if (CheckForElementDeletion()) {
                    UpdateCalendar();
                    UpdateBusinessObjects();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);

                Common.ShowGenericErrorMessage();
            }
        }

        #endregion

    }

}
