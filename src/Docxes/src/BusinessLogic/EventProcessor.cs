using System;
using System.Collections.Generic;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides functionality to process events.
    /// </summary>
    public sealed class EventProcessor : BusinessObjectProcessor<Event, Subject> {

        /// <summary>
        /// Creates a new instance of the class <see cref="EventProcessor"/>.
        /// </summary>
        public EventProcessor() {
            dataManager = new EventsDataManager();
        }


        /// <summary>
        /// Indicates whether a new object can be saved.
        /// </summary>
        /// <param name="school">The parent of the object, on which the ability to save a new object is based on.</param>
        /// <returns>True if a new object can be saved; otherwise, false.</returns>
        public override bool CanCreate(IBusinessObject school) {
            if (school == null) {
                throw new ArgumentNullException("school");
            }
            
            School schoolAsSchool;
            try {
                schoolAsSchool = (School)school;
            }
            catch {
                throw new ArgumentException("parameter \"school\" cannot be converted to type \"School\"");
            }

            var subjectProcessor = new BusinessLogic.SubjectProcessor();
            return subjectProcessor.Get(schoolAsSchool).Count > 0;
        }

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public override void Create(Event objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public override List<Event> Get() {
            return dataManager.Get();
        }

        /// <summary>
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public override List<Event> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }

        /// <summary>
        /// Gets all existing entities with the specified date.
        /// </summary>
        /// <param name="date">The date that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified date.</returns>
        public List<Event> Get(School school, DateTime date) {
            var subjectProcessor = new SubjectProcessor();
            var subjects = subjectProcessor.Get(school);

            List<Event> events = new List<Event>();
            foreach (var subject in subjects) {
                events.AddRange(((EventsDataManager)dataManager).Get(subject, date));
            }

            return events;
        }

        /// <summary>
        /// Gets all existing entities between the specified minimum and maximum date.
        /// </summary>
        /// <param name="minimumDate">The minimum date that the returned entities can have (inclusive).</param>
        /// <param name="maximumDate">The maximum date that the returned entities can have (inclusive).</param>
        /// <returns>A list of all existing entities between the specified minimum and maximum date.</returns>
        public List<Event> Get(School school, DateTime minimumDate, DateTime maximumDate) {
            var subjectProcessor = new SubjectProcessor();
            var subjects = subjectProcessor.Get(school);

            List<Event> events = new List<Event>();
            foreach (var subject in subjects) {
                events.AddRange(((EventsDataManager)dataManager).Get(subject, minimumDate, maximumDate));
            }

            return events;
        }


        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public override void Update(Event objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
        public override void Delete(Event objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
