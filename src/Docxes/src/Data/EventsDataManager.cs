using System;
using System.Collections.Generic;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage events in nonvolatile memory.
    /// </summary>
    internal sealed class EventsDataManager : BusinessObjectDataManager<Event, Subject> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        internal override void Create(Event entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            databaseContainer.Events.Add(entityToSave);

            databaseContainer.SaveChanges();
        }


        private List<Event> Get(Predicate<Event> predicate) {
            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            return (from
                        Event entity
                    in
                        databaseContainer.Events
                    orderby
                        entity.Name ascending
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        internal override List<Event> Get() {
            return Get(entity => true);
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        internal override List<Event> Get(Subject entitiesParent) {
            if (entitiesParent == null) {
                throw new ArgumentNullException("entitiesParent");
            }

            return Get(entity => entity.Subject.Equals(entitiesParent));
        }

        /// <summary>
        /// Gets all existing entities with the specified date.
        /// </summary>
        /// <param name="subject">The subject that the returned entities must have.</param>
        /// <param name="date">The date that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified date.</returns>
        internal List<Event> Get(Subject subject, DateTime date) {
            return Get(entity => entity.Date.Date == date.Date && entity.Subject.Equals(subject));
        }

        /// <summary>
        /// Gets all existing entities between the specified minimum and maximum date.
        /// </summary>
        /// <param name="subject">The subject that the returned entities must have.</param>
        /// <param name="minimumDate">The minimum date that the returned entities can have (inclusive).</param>
        /// <param name="maximumDate">The maximum date that the returned entities can have (inclusive).</param>
        /// <returns>A list of all existing entities between the specified minimum and maximum date.</returns>
        internal List<Event> Get(Subject subject, DateTime minimumDate, DateTime maximumDate) {
            return Get(entity => entity.Date.Date >= minimumDate.Date && entity.Date.Date <= maximumDate.Date && entity.Subject.Equals(subject));
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        internal override void Update(Event entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToUpdate = databaseContainer.Events.Find(entityToUpdate.Id);
            databaseContainer.Entry(databaseObjectToUpdate).CurrentValues.SetValues(entityToUpdate);

            databaseContainer.SaveChanges();
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        internal override void Delete(Event entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToDelete = databaseContainer.Events.Find(entityToDelete.Id);
            databaseContainer.Events.Remove(databaseObjectToDelete);

            databaseContainer.SaveChanges();
        }

    }

}
