namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, FilePath: {FilePath}, Subject: {Subject}")]
    public partial class Document : IBusinessObject {

        public Document() {
            // Required for LINQ
        }

        public Document(string filePath, Subject subject) {
            FilePath = filePath;
            Subject = subject;
        }

        public Document(Document businessObjectEditing, string filePath, Subject subject)
            : this(filePath, subject) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            // TODO:

            //return Name;

            return FilePath;
        }

    }

}
