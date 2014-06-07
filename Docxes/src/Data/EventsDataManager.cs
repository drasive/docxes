using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage events in nonvolatile memory.
    /// </summary>
    public sealed class EventsDataManager : BusinessObjectDataManager<Event, Subject> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
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
                            .Include(entity => entity.Subject)
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public override List<Event> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        public override List<Event> Get(Subject entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            }
        }

        /// <summary>
        /// Gets all existing entities with the specified date.
        /// </summary>
        /// <param name="date">The date that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified date.</returns>
        public List<Event> Get(DateTime date) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Date.Date == date.Date);
            }
        }

        /// <summary>
        /// Gets all existing entities between the specified minimum and maximum date.
        /// </summary>
        /// <param name="minimumDate">The minimum date that the returned entities can have (inclusive).</param>
        /// <param name="maximumDate">The maximum date that the returned entities can have (inclusive).</param>
        /// <returns>A list of all existing entities between the specified minimum and maximum date.</returns>
        public List<Event> Get(DateTime minimumDate, DateTime maximumDate) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Date.Date >= minimumDate.Date && entity.Date.Date <= maximumDate.Date);
            }
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
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


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
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
