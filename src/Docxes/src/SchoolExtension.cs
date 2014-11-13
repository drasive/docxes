using System;
using System.Collections.Generic;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a school.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Comment: {Comment}")]
    public partial class School : IBusinessObject, IEquatable<School> {

        /// <summary>
        /// Creates a new instance of the class <see cref="School"/> with the specified name and comment.
        /// </summary>
        /// <param name="name">The name of the school.</param>
        /// <param name="comment">The comment of the school.</param>
        public School(string name, string comment) {
            // Initialize relations
            Teachers = new List<Teacher>();

            // Assign properties
            Name = name;
            Comment = comment;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="School"/> with the specified name, comment and the id of the business object editing.
        /// </summary>
        /// <param name="name">The name of the school.</param>
        /// <param name="comment">The comment of the school.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public School(string name, string comment, School businessObjectEditing)
            : this(name, comment) {
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
        /// Determines whether the specified school is equal to the current school.
        /// </summary>
        /// <param name="schoolToEquate">The school to compare with the current school.</param>
        /// <returns>True if the specified school is equal to the current school; otherwise, false.</returns>
        public bool Equals(School schoolToEquate) {
            return schoolToEquate != null && Id == schoolToEquate.Id;
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

            var schoolToEquate = objectToEquate as School;
            return (schoolToEquate != null && Equals(schoolToEquate));
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
