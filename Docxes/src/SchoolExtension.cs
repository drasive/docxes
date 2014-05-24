using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Comment: {Comment}")]
    public partial class School : IBusinessObject, IEquatable<School> {

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

        public bool Equals(School schoolToEquate) {
            return schoolToEquate != null && Id == schoolToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var schoolToEquate = objectToEquate as School;
            return (schoolToEquate != null && Equals(schoolToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
