namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, First name: {FirstName}, Last name: {LastName}, Is male: {IsMale}, School: {School}")]
    public partial class Teacher : IBusinessObject {

        public Teacher(string firstName, string lastName, bool isMale, School school) {
            FirstName = firstName;
            LastName = lastName;
            IsMale = isMale;
            School = school;
        }

        public Teacher(Teacher businessObjectEditing, string firstName, string lastName, bool isMale, School school)
            : this(firstName, lastName, isMale, school) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return FirstName + " " + LastName;
        }

    }

}
