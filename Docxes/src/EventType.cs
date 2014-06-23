using System.ComponentModel;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a type of an event.
    /// </summary>
    public enum EventType {
        [Description("Anderer")]
        Other,
        [Description("Hausaufgabe")]
        Homework,
        [Description("Abgabe")]
        Submission,
        [Description("Test")]
        Test
    }

}
