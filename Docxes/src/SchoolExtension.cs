using System;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Comment: {Comment}")]
    public partial class School : IBusinessObject {

        public School(string name, string comment) {
            Name = name;
            Comment = comment;
        }

        public School(int id, string name, string comment)
            : this(name, comment) {
            Id = id;
        }

        public override string ToString() {
            return Name;
        }

    }

}
