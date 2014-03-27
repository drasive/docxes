using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Date: {Date}, Type: {Type}, Subject: {Subject}")]
    public partial class Event : IBusinessObject {

        public Event(string name, DateTime date, int type, Subject subject) {
            Name = name;
            Date = date;
            Type = type;
            Subject = subject;
        }

        public Event(Event businessObjectEditing, string name, DateTime date, int type, Subject subject)
            : this(name, date, type, subject) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
