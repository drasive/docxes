namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a grade.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Value: {Value}, Weight: {Weight}, Comment: {Comment}, Subject: {Subject}, Event: {Event}")]
    public partial class Grade : IBusinessObject {

        /// <summary>
        /// Creates a new instance of the class <see cref="Grade"/>.
        /// </summary>
        public Grade() {
            // Required for LINQ
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Grade"/> with the specified value, weight, comment, subject and event.
        /// </summary>
        /// <param name="value">The value of the grade.</param>
        /// <param name="weight">The weight of the grade.</param>
        /// <param name="comment">The comment of the grade.</param>
        /// <param name="subject">The subject of the grade.</param>
        public Grade(decimal value, int weight, string comment, Subject subject) {
            Value = value;
            Weight = weight;
            Comment = comment;

            SubjectId = subject.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Grade"/> with the specified value, weight, comment, subject, event and the id of the business object editing.
        /// </summary>
        /// <param name="value">The value of the grade.</param>
        /// <param name="weight">The weight of the grade.</param>
        /// <param name="comment">The comment of the grade.</param>
        /// <param name="subject">The subject of the grade.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Grade(decimal value, int weight, string comment, Subject subject, Grade businessObjectEditing)
            : this(value, weight, comment, subject) {
            Id = businessObjectEditing.Id;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return Value.ToString();
        }

        /// <summary>
        /// Determines whether the specified grade is equal to the current grade.
        /// </summary>
        /// <param name="gradeToEquate">The grade to compare with the current grade.</param>
        /// <returns>True if the specified grade is equal to the current grade; otherwise, false.</returns>
        public bool Equals(Grade gradeToEquate) {
            return gradeToEquate != null && Id == gradeToEquate.Id;
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

            var gradeToEquate = objectToEquate as Grade;
            return (gradeToEquate != null && Equals(gradeToEquate));
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
