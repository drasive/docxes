namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Comment: {Comment}")]
    public partial class School : IBusinessObject {

        public School(string name, string comment) {
            Name = name;
            Comment = comment;
        }

        public School(School businessObjectEditing, string name, string comment)
            : this(name, comment) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
