using System;
using System.IO;

namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, FilePath: {FilePath}, Subject: {Subject}")]
    public partial class Document : IBusinessObject {

        // TODO:
        private Cache<FileInfo> fileInfo = new Cache<FileInfo>(() => new System.IO.FileInfo("" /*FilePath*/));


        public bool Exists { get { return fileInfo.Value.Exists; } }
        public string Name { get { return fileInfo.Value.Name; } }
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

    }

}
