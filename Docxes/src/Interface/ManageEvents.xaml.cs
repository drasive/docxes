﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="ManageEvents.xaml"/>
    /// </summary>
    public sealed partial class ManageEvents : Window {

        // TODO: Multi date selection, grid intead of list
        private DateTime _lastDisplayDate;

        private BusinessLogic.BusinessObjectProcessor<Subject, Teacher> businessObjectParentProcessor = new BusinessLogic.SubjectProcessor();
        private BusinessLogic.BusinessObjectProcessor<Event, Subject> businessObjectProcessor = new BusinessLogic.EventProcessor();


        public ManageEvents() {
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
            for (int dayOfMonth = 1; dayOfMonth <= DateTime.DaysInMonth(cEvents.DisplayDate.Year, cEvents.DisplayDate.Month); dayOfMonth++) {
                var currentDate = new DateTime(cEvents.DisplayDate.Year, cEvents.DisplayDate.Month, dayOfMonth);
                List<Event> events = ((BusinessLogic.EventProcessor)businessObjectProcessor).Get(currentDate);

                if (events.Count > 0) {
                    // Mark day as highlighted
                    cEvents.HighlightedDateText[dayOfMonth - 1] = events.Count.ToString();
                }
            }

            // Refresh calendar
            cEvents.Refresh();
        }


        private Subject SelectedBusinessObjectParent { get { return SelectedBusinessObject.Subject; } }

        private Event SelectedBusinessObject { get { return (Event)lbEvents.SelectedItem; } }

        private void UpdateBusinessObjects() {
            IEnumerable<Event> businessObjects = ((BusinessLogic.EventProcessor)businessObjectProcessor).Get(SelectedDate);

            if (businessObjects.Count() > 0) {
                lbEvents.ItemsSource = businessObjects;
            }
            else {
                ListBoxItem noBusinessObjectsPlaceholder = new ListBoxItem() {
                    Content = "Keine Events für dieses Datum vorhanden.\nKlicken Sie auf \"Hinzufügen\" um ein neues Ereignis zu erstellen.",
                    FontSize = 10,
                    IsEnabled = false
                };
                lbEvents.ItemsSource = new List<ListBoxItem>() { noBusinessObjectsPlaceholder };
            }
        }


        private BusinessObjectManagerAction OpenAddBusinessObjectManager() {
            Window addBusinessObjectManager = new ManageEvent() { Owner = this };
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
                businessObjectProcessor.Delete((Event)lbEvents.SelectedItem);
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
            UpdateCalendar();
            UpdateBusinessObjects();
            UpdateControlsAvailability();
        }


        private void cEvents_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            UpdateBusinessObjects();
        }

        private void cEvents_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e) {
            bool isCalendarRefreshRequired = cEvents.DisplayDate != null && cEvents.DisplayDate != DateTime.MinValue && cEvents.DisplayDate.Date != _lastDisplayDate.Date;
            if (isCalendarRefreshRequired) {
                _lastDisplayDate = cEvents.DisplayDate;

                UpdateCalendar();
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e) {
            base.OnPreviewMouseUp(e);

            // Required to remove the necessity for an additional click to loose focus of a calendar
            if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem) {
                Mouse.Capture(null);
            }
        }


        private void lbEvents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateControlsAvailability();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            if (OpenAddBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                UpdateBusinessObjects();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            if (OpenEditBusinessObjectManager() == BusinessObjectManagerAction.Saved) {
                UpdateCalendar();
                UpdateBusinessObjects();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (CheckForElementDeletion()) {
                UpdateCalendar();
                UpdateBusinessObjects();
            }
        }

        #endregion

    }

}
