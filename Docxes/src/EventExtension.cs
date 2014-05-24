using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Date: {Date}, Type: {Type}, Comment: {Comment}, Subject: {Subject}")]
    public partial class Event : IBusinessObject {

        public Event(string name, DateTime date, int type, string comment, Subject subject) {
            Name = name;
            Date = date;
            Type = type;
            Comment = comment;
            SubjectId = subject.Id;
        }

        public Event(Event businessObjectEditing, string name, DateTime date, int type, string comment, Subject subject)
            : this(name, date, type, comment, subject) {
            // TODO: Change order of parameters in all of these classes
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

        public bool Equals(Event eventToEquate) {
            return eventToEquate != null && Id == eventToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var eventToEquate = objectToEquate as Event;
            return (eventToEquate != null && Equals(eventToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
