using System.Windows;
using System.Windows.Controls;

namespace VrankenBischof.Docxes.Interface {

    /// <summary>
    /// Interaction logic for <see cref="SubjectOverview.xaml"/>.
    /// </summary>
    internal partial class SubjectOverview : UserControl {

        internal SubjectOverview() {
            InitializeComponent();
        }


        internal event ManagingDocumentsDelegate ManagingDocuments;
        internal event ManagingNotesDelegate ManagingNotes;
        internal event ManagingGradesDelegate ManagingGrades;
        internal event ManagingEventsDelegate ManagingEvents;

        internal event AddingDocumentDelegate AddingDocument;
        internal event AddingNoteDelegate AddingNote;
        internal event AddingGradeDelegate AddingGrade;
        internal event AddingEventDelegate AddingEvent;

        #region Event raising

        internal delegate void ManagingDocumentsDelegate(object sender, RoutedEventArgs e);
        internal delegate void ManagingNotesDelegate(object sender, RoutedEventArgs e);
        internal delegate void ManagingGradesDelegate(object sender, RoutedEventArgs e);
        internal delegate void ManagingEventsDelegate(object sender, RoutedEventArgs e);

        protected void OnManagingDocuments(RoutedEventArgs e) {
            var handler = ManagingDocuments;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnManagingNotes(RoutedEventArgs e) {
            var handler = ManagingNotes;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnManagingGrades(RoutedEventArgs e) {
            var handler = ManagingGrades;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnManagingEvents(RoutedEventArgs e) {
            var handler = ManagingEvents;
            if (handler != null) {
                handler(this, e);
            }
        }


        internal delegate void AddingDocumentDelegate(object sender, RoutedEventArgs e);
        internal delegate void AddingNoteDelegate(object sender, RoutedEventArgs e);
        internal delegate void AddingGradeDelegate(object sender, RoutedEventArgs e);
        internal delegate void AddingEventDelegate(object sender, RoutedEventArgs e);

        protected void OnAddingDocument(RoutedEventArgs e) {
            var handler = AddingDocument;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnAddingNote(RoutedEventArgs e) {
            var handler = AddingNote;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnAddingGrade(RoutedEventArgs e) {
            var handler = AddingGrade;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void OnAddingEvent(RoutedEventArgs e) {
            var handler = AddingEvent;
            if (handler != null) {
                handler(this, e);
            }
        }

        #endregion

        #region Event wiring

        protected void btnManageDocuments_Click(object sender, RoutedEventArgs e) {
            OnManagingDocuments(e);
        }

        protected void btnManageNotes_Click(object sender, RoutedEventArgs e) {
            OnManagingNotes(e);
        }

        protected void btnManageGrades_Click(object sender, RoutedEventArgs e) {
            OnManagingGrades(e);
        }

        protected void btnManageEvents_Click(object sender, RoutedEventArgs e) {
            OnManagingEvents(e);
        }


        protected void btnAddDocument_Click(object sender, RoutedEventArgs e) {
            OnAddingDocument(e);
        }

        protected void btnAddNote_Click(object sender, RoutedEventArgs e) {
            OnAddingNote(e);
        }

        protected void btnAddGrade_Click(object sender, RoutedEventArgs e) {
            OnAddingGrade(e);
        }

        protected void btnAddEvent_Click(object sender, RoutedEventArgs e) {
            OnAddingEvent(e);
        }

        #endregion

    }
}
