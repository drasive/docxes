using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Date: {Date}, Type: {Type}, Comment: {Comment}, Subject: {Subject}")]
    public partial class Event : IBusinessObject {

        public Event(string name, DateTime date, int type, string comment, Subject subject) {
            Name = name;
            Date = date;
            Type = type;
            Comment = comment;
            Subject = subject;
        }

        public Event(Event businessObjectEditing, string name, DateTime date, int type, string comment, Subject subject)
            : this(name, date, type, comment, subject) {
            // TODO: Change order of parameters in all of these classes
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
