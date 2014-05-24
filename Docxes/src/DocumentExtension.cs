using System;
using System.IO;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, FilePath: {FilePath}, Subject: {Subject}")]
    public partial class Document : IBusinessObject, IEquatable<Document> {

        // TODO:
        private FileInfo fileInfoCached;
        private FileInfo FileInfo {
            get {
                if (fileInfoCached == null) {
                    fileInfoCached = new System.IO.FileInfo(FilePath);
                }

                return fileInfoCached;
            }
        }

        public bool DoesExist { get { return FileInfo.Exists; } }
        public string Name { get { return FileInfo.Name; } }
        public string Extension { get { return FileInfo.Extension; } }
        public long Size { get { return FileInfo.Length / 1024; } }

        public DateTime LastWriteTime { get { return FileInfo.LastWriteTime; } }
        public DateTime CreationTime { get { return FileInfo.CreationTime; } }
        public DateTime LastAccessTime { get { return FileInfo.LastAccessTime; } }


        public Document() {
            // Required for LINQ
        }

        public Document(string filePath, Subject subject) {
            FilePath = filePath;
            SubjectId = subject.Id;
        }

        public Document(Document businessObjectEditing, string filePath, Subject subject)
            : this(filePath, subject) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Name;
        }

        public bool Equals(Document documentToEquate) {
            return documentToEquate != null && Id == documentToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var documentToEquate = objectToEquate as Document;
            return (documentToEquate != null && Equals(documentToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
