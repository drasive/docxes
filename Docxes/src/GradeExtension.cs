namespace VrankenBischof.Docxes {

    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Value: {Value}, Weight: {Weight}, Comment: {Comment}, Subject: {Subject}, Event: {Event}")]
    public partial class Grade : IBusinessObject {

        public Grade() {
            // Required for LINQ
        }

        public Grade(int value, int weight, string comment, Subject subject, Event @event) {
            Value = value;
            Weight = weight;
            Comment = comment;
            SubjectId = subject.Id;
            EventId = @event.Id;
        }

        public Grade(Grade businessObjectEditing, int value, int weight, string comment, Subject subject, Event @event)
            : this(value, weight, comment, subject, @event) {
            Id = businessObjectEditing.Id;
        }


        public override string ToString() {
            return Value.ToString();
        }

        public bool Equals(Grade gradeToEquate) {
            return gradeToEquate != null && Id == gradeToEquate.Id;
        }

        public override bool Equals(object objectToEquate) {
            if (objectToEquate == null) {
                return false;
            }

            var gradeToEquate = objectToEquate as Grade;
            return (gradeToEquate != null && Equals(gradeToEquate));
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }

}
