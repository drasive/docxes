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

    }

}
