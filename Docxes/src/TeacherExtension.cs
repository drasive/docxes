using System;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a teacher.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, First name: {FirstName}, Last name: {LastName}, Is male: {IsMale}, School: {School}")]
    public partial class Teacher : IBusinessObject, IEquatable<Teacher> {

        /// <summary>
        /// Creates a new instance of the class <see cref="Teacher"/> with the specified first name, last name, gender and school.
        /// </summary>
        /// <param name="firstName">The first name of the teacher.</param>
        /// <param name="lastName">The last name of the teacher.</param>
        /// <param name="isMale">The gender of the teacher.</param>
        /// <param name="school">The school of the teacher.</param>
        public Teacher(string firstName, string lastName, bool isMale, School school) {
            FirstName = firstName;
            LastName = lastName;
            IsMale = isMale;

            SchoolId = school.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Teacher"/> with the specified first name, last name, gender, school and the id of the business object editing.
        /// </summary>
        /// <param name="firstName">The first name of the teacher.</param>
        /// <param name="lastName">The last name of the teacher.</param>
        /// <param name="isMale">The gender of the teacher.</param>
        /// <param name="school">The school of the teacher.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Teacher(string firstName, string lastName, bool isMale, School school, Teacher businessObjectEditing)
            : this(firstName, lastName, isMale, school) {
            Id = businessObjectEditing.Id;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return FirstName + " " + LastName;
        }

        /// <summary>
        /// Determines whether the specified teacher is equal to the current teacher.
        /// </summary>
        /// <param name="teacherToEquate">The teacher to compare with the current teacher.</param>
        /// <returns>True if the specified teacher is equal to the current teacher; otherwise, false.</returns>
        public bool Equals(Teacher teacherToEquate) {
            return teacherToEquate != null && Id == teacherToEquate.Id;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="objectToEquate">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var teacherToEquate = objectToEquate as Teacher;
            return (teacherToEquate != null && Equals(teacherToEquate));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
