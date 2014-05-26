using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class EventProcessor : BusinessObjectProcessor<Event, Subject> {

        public EventProcessor() {
            dataManager = new EventsDataManager();
        }


        //public override bool CanCreate() {
        //    return true;
        //}

        public override void Create(Event objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Event> Get( ) {
            return dataManager.Get();
        }
        
        public override List<Event> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }

        public IEnumerable<EventType> GetTypes() {
            var eventTypes = new List<EventType>();

            foreach (int eventType in ((EventsDataManager)dataManager).GetTypes()) {
                eventTypes.Add((EventType)eventType);
            }

            return eventTypes;
        }


        public override void Update(Event objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


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
