namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Content: {Content}, Subject: {Subject}")]
    public partial class Note : IBusinessObject {

        public Note() {
            // Required for LINQ
        } 

        public Note(string name, string content, Subject subject) {
            Name = name;
            Content = content;
            Subject = subject;
        }

        public Note(Note businessObjectEditing, string name, string content, Subject subject)
            : this(name, content, subject) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
