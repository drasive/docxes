using System;
using System.IO;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a document.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, FilePath: {FilePath}, Subject: {Subject}")]
    public partial class Document : IBusinessObject, IEquatable<Document> {

        protected FileInfo FileInfo { get { return new System.IO.FileInfo(FilePath); } }

        public bool DoesExist { get { return FileInfo.Exists; } }
        public string Name { get { return FileInfo.Name; } }
        public string Extension { get { return FileInfo.Extension; } }
        public long Size { get { return FileInfo.Length / 1024; } }
        public string Directory { get { return FileInfo.DirectoryName; } }

        public DateTime LastWriteTime { get { return FileInfo.LastWriteTime; } }
        public DateTime CreationTime { get { return FileInfo.CreationTime; } }
        public DateTime LastAccessTime { get { return FileInfo.LastAccessTime; } }


        // UI formatting
        public string LastWriteTimeAsString { get { return LastWriteTime.ToString("HH:mm:ss dd.MM.yyyy"); } }
        public string CreationTimeAsString { get { return CreationTime.ToString("HH:mm:ss dd.MM.yyyy"); } }
        public string LastAccessTimeAsString { get { return LastAccessTime.ToString("HH:mm:ss dd.MM.yyyy"); } }


        /// <summary>
        /// Creates a new instance of the class <see cref="Document"/>.
        /// </summary>
        public Document() {
            // Required for LINQ
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Document"/> with the specified file path and subject.
        /// </summary>
        /// <param name="filePath">The file path of the document.</param>
        /// <param name="subject">The parent of the document.</param>
        public Document(string filePath, Subject subject) {
            FilePath = filePath;

            SubjectId = subject.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Document"/> with the specified file path, subject and the id of the business object editing.
        /// </summary>
        /// <param name="filePath">The file path of the document.</param>
        /// <param name="subject">The parent of the document.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Document(string filePath, Subject subject, Document businessObjectEditing)
            : this(filePath, subject) {
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
        /// Determines whether the specified document is equal to the current document.
        /// </summary>
        /// <param name="documentToEquate">The document to compare with the current document.</param>
        /// <returns>True if the specified document is equal to the current document; otherwise, false.</returns>
        public bool Equals(Document documentToEquate) {
            return documentToEquate != null && Id == documentToEquate.Id;
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

            var documentToEquate = objectToEquate as Document;
            return (documentToEquate != null && Equals(documentToEquate));
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
