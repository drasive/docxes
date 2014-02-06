namespace VrankenBischof.Docxes {

    public partial class Teacher : IManagementElement {

        public Teacher(string firstName, string lastName, bool isMale) {
            FirstName = firstName;
            LastName = lastName;
            IsMale = isMale;
        }

    }

}
