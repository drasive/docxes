namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a subject.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Teacher: {Teacher}")]
    public partial class Subject : IBusinessObject {

        /// <summary>
        /// Creates a new instance of the class <see cref="Subject"/> with the specified name and teacher.
        /// </summary>
        /// <param name="name">The name of the subject.</param>
        /// <param name="teacher">The teacher of the subject.</param>
        public Subject(string name, Teacher teacher) {
            Name = name;

            TeacherId = teacher.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Subject"/> with the specified name, teacher and the id of the business object editing.
        /// </summary>
        /// <param name="name">The name of the subject.</param>
        /// <param name="teacher">The teacher of the subject.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Subject(string name, Teacher teacher, Subject businessObjectEditing)
            : this(name, teacher) {
            Id = businessObjectEditing.Id;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return Name;
        }

        /// <summary>
        /// Determines whether the specified subject is equal to the current subject.
        /// </summary>
        /// <param name="subjectToEquate">The subject to compare with the current subject.</param>
        /// <returns>True if the specified subject is equal to the current subject; otherwise, false.</returns>
        public bool Equals(Subject subjectToEquate) {
            return subjectToEquate != null && Id == subjectToEquate.Id;
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

            var subjectToEquate = objectToEquate as Subject;
            return (subjectToEquate != null && Equals(subjectToEquate));
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
