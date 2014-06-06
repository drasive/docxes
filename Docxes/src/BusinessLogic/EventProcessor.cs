﻿using System;
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


        //public override bool CanCreate() {
        //    return true;
        //}

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
        public override List<Event> Get( ) {
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
        public List<Event> Get(DateTime date) {
            return ((EventsDataManager)dataManager).Get(date);
        }

        /// <summary>
        /// Gets all event types.
        /// </summary>
        /// <returns>All event types.</returns>
        public IEnumerable<EventType> GetTypes() {
            var values = Enum.GetValues(typeof(EventType));
            EventType[] array = (EventType[])values;

            return array;
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

            // Remove dependencies
            var gradeProcessor = new GradeProcessor();
            foreach (Grade dependencyToDisconnect in gradeProcessor.Get(objectToDelete)) {
                // TODO: Check if EventId needs to be modified too
                dependencyToDisconnect.Event = null;
                gradeProcessor.Update(dependencyToDisconnect);
            }

            // Delete object
            dataManager.Delete(objectToDelete);
        }

    }

}
