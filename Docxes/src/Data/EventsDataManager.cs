using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class EventsDataManager : BusinessObjectDataManager<Event> {

        public override void Create(Event objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Events.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Event> Get(LocalDatabaseContainer container) {
            return (from Event Event
                    in container.Events
                    select Event).ToList();
        }

        public override List<Event> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override Event Get(int id) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Event objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Events.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Events.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Event objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Events.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
