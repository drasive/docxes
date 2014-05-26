namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Teacher: {Teacher}")]
    public partial class Subject : IBusinessObject {

        public Subject(string name, Teacher teacher) {
            Name = name;

            TeacherId = teacher.Id;
        }

        public Subject(Subject businessObjectEditing, string name, Teacher teacher)
            : this(name, teacher) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

        public bool Equals(Subject subjectToEquate) {
            return subjectToEquate != null && Id == subjectToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var subjectToEquate = objectToEquate as Subject;
            return (subjectToEquate != null && Equals(subjectToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
