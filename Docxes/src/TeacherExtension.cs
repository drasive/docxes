using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, First name: {FirstName}, Last name: {LastName}, Is male: {IsMale}, School: {School}")]
    public partial class Teacher : IBusinessObject, IEquatable<Teacher> {

        public Teacher(string firstName, string lastName, bool isMale, School school) {
            FirstName = firstName;
            LastName = lastName;
            IsMale = isMale;
            SchoolId = school.Id;
        }

        public Teacher(Teacher businessObjectEditing, string firstName, string lastName, bool isMale, School school)
            : this(firstName, lastName, isMale, school) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return FirstName + " " + LastName;
        }

        public bool Equals(Teacher teacherToEquate) {
            return teacherToEquate != null && Id == teacherToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var teacherToEquate = objectToEquate as Teacher;
            return (teacherToEquate != null && Equals(teacherToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
