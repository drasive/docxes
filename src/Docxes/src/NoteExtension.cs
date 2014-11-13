namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a note.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Content: {Content}, Subject: {Subject}")]
    public partial class Note : IBusinessObject {

        /// <summary>
        /// Creates a new instance of the class <see cref="Note"/>.
        /// </summary>
        public Note() {
            // Required for LINQ
        } 

        /// <summary>
        /// Creates a new instance of the class <see cref="Note"/> with the specified name, content and subject.
        /// </summary>
        /// <param name="name">The name of the note.</param>
        /// <param name="content">The content of the note.</param>
        /// <param name="subject">The subject of the note.</param>
        public Note(string name, string content, Subject subject) {
            Name = name;
            Content = content;

            SubjectId = subject.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Note"/> with the specified name, content, subject and the id of the business object editing.
        /// </summary>
        /// <param name="name">The name of the note.</param>
        /// <param name="content">The content of the note.</param>
        /// <param name="subject">The subject of the note.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Note(string name, string content, Subject subject, Note businessObjectEditing)
            : this(name, content, subject) {
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
        /// Determines whether the specified note is equal to the current note.
        /// </summary>
        /// <param name="noteToEquate">The note to compare with the current note.</param>
        /// <returns>True if the specified note is equal to the current note; otherwise, false.</returns>
        public bool Equals(Note noteToEquate) {
            return noteToEquate != null && Id == noteToEquate.Id;
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

            var noteToEquate = objectToEquate as Note;
            return (noteToEquate != null && Equals(noteToEquate));
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
