namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Teacher: {Teacher}")]
    public partial class Subject : IBusinessObject {

        public Subject(string name, Teacher teacher) {
            Name = name;
            Teacher = teacher;
        }

        public Subject(Subject businessObjectEditing, string name, Teacher teacher)
            : this(name, teacher) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

    }

}
