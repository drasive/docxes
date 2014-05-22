namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Comment: {Comment}, Subject: {Subject}, Event: {Event}")]
    public partial class Document : IBusinessObject {

        public Document() {
            // Required for LINQ
        }   

        public Document(string name, byte[] content, string comment, Subject subject, Event @event) {
            Name = name;
            Content = content;
            Comment = comment;
            Subject = subject;
            // TODO:
            //Event = @event;
        }

        public Document(Document businessObjectEditing, string name, byte[] content, string comment, Subject subject, Event @event)
            : this(name, content, comment, subject, @event) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
