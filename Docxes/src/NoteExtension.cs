namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Content: {Content}, Subject: {Subject}, Event: {Event}")]
    public partial class Note : IBusinessObject {

        public Note() {
            // Required for LINQ
        } 

        public Note(string name, string content, Subject subject, Event @event) {
            Name = name;
            Content = content;
            Subject = subject;
            Event = @event;
        }

        public Note(Note businessObjectEditing, string name, string content, Subject subject, Event @event)
            : this(name, content, subject, @event) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
