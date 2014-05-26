namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Content: {Content}, Subject: {Subject}")]
    public partial class Note : IBusinessObject {

        public Note() {
            // Required for LINQ
        } 

        public Note(string name, string content, Subject subject) {
            Name = name;
            Content = content;

            SubjectId = subject.Id;
        }

        public Note(Note businessObjectEditing, string name, string content, Subject subject)
            : this(name, content, subject) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

        public bool Equals(Note noteToEquate) {
            return noteToEquate != null && Id == noteToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var noteToEquate = objectToEquate as Note;
            return (noteToEquate != null && Equals(noteToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
