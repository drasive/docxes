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

            var container = GetDatabaseContainer();
            container.Events.Add(objectToSave);
            container.SaveChanges();
        }


        private List<Event> Get(LocalDatabaseContainer container) {
            return (from Event Event
                    in container.Events
                    select Event).ToList();
        }

        public override List<Event> Get() {
            var container = GetDatabaseContainer();
            return Get(container);
        }

        public override Event Get(int id) {
            var container = GetDatabaseContainer();
            return Get(container).First(databaseElement => databaseElement.Id == id);
        }


        public override void Update(Event objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
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

            var container = GetDatabaseContainer();
            var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
            container.Events.Remove(databaseObjectToDelete);
            container.SaveChanges();
        }

    }

}
