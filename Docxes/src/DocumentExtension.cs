using System;
using System.IO;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, FilePath: {FilePath}, Subject: {Subject}")]
    public partial class Document : IBusinessObject, IEquatable<Document> {

        // TODO:
        private Cache<FileInfo> fileInfo = new Cache<FileInfo>(() => { return new System.IO.FileInfo("C:/temp" /*FilePath*/); });

        public bool DoesExist { get { return fileInfo.Value.Exists; } }
        public string Name { get { return fileInfo.Value.Name; } }
        public string Extension { get { return fileInfo.Value.Extension; } }
        public long Size { get { return fileInfo.Value.Length / 1024; } }

        public DateTime LastWriteTime { get { return fileInfo.Value.LastWriteTime; } }
        public DateTime CreationTime { get { return fileInfo.Value.CreationTime; } }
        public DateTime LastAccessTime { get { return fileInfo.Value.LastAccessTime; } }


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
