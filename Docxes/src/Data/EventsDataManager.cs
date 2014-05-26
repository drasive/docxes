using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class EventsDataManager : BusinessObjectDataManager<Event, Subject> {

        public override void Create(Event entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Events.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Event> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }
        
        private List<Event> Get(LocalDatabaseContainer databaseContainer, Predicate<Event> predicate) {
            return (from
                        Event entity
                    in
                        databaseContainer.Events
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        public override List<Event> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override List<Event> Get(Subject entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            }
        }

        public List<int> GetTypes() {
            return new List<int>{0, 1, 2, 3};
        }


        public override void Update(Event entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Events.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        public override void Delete(Event entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Events.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
