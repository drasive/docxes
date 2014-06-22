using System;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents an event.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id: {Id}, Name: {Name}, Place: {Place}, Date: {Date}, Type: {Type}, Comment: {Comment}, Subject: {Subject}")]
    public partial class Event : IBusinessObject {

        /// <summary>
        /// Creates a new instance of the class <see cref="Event"/>.
        /// </summary>
        public Event() {
            // Required for LINQ
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Event"/> with the specified name, place, date, type, comment and subject.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="place">The place of the event.</param>
        /// <param name="date">The date of the event.</param>
        /// <param name="type">The type of the event.</param>
        /// <param name="comment">The comment of the event.</param>
        /// <param name="subject">The subject of the event.</param>
        public Event(string name, string place, DateTime date, int type, string comment, Subject subject) {
            Name = name;
            Place = place;
            Date = date;
            Type = type;
            Comment = comment;

            SubjectId = subject.Id;
        }

        /// <summary>
        /// Creates a new instance of the class <see cref="Event"/> with the specified name, place, date, type, comment, subject and the id of the business object editing..
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="place">The place of the event.</param>
        /// <param name="date">The date of the event.</param>
        /// <param name="type">The type of the event.</param>
        /// <param name="comment">The comment of the event.</param>
        /// <param name="subject">The subject of the event.</param>
        /// <param name="businessObjectEditing">The business object editing to take the id from.</param>
        public Event(string name, string place, DateTime date, int type, string comment, Subject subject, Event businessObjectEditing)
            : this(name, place, date, type, comment, subject) {
            Id = businessObjectEditing.Id;
        }


        // UI formatting
        // TODO: Translate to german (enum desc)
        public string TypeAsString { get { return Enum.GetName(typeof(EventType), Type); } }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return Name;
        }

        /// <summary>
        /// Determines whether the specified event is equal to the current event.
        /// </summary>
        /// <param name="eventToEquate">The event to compare with the current event.</param>
        /// <returns>True if the specified event is equal to the current event; otherwise, false.</returns>
        public bool Equals(Event eventToEquate) {
            return eventToEquate != null && Id == eventToEquate.Id;
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

            var eventToEquate = objectToEquate as Event;
            return (eventToEquate != null && Equals(eventToEquate));
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
