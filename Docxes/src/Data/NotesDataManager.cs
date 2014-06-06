using System;
using System.Collections.Generic;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage notes in nonvolatile memory.
    /// </summary>
    public sealed class NotesDataManager : BusinessObjectDataManager<Note, Subject> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        public override void Create(Note entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Notes.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Note> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<Note> Get(LocalDatabaseContainer databaseContainer, Predicate<Note> predicate) {
            return (from
                        Note entity
                    in
                        databaseContainer.Notes
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public override List<Note> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        public override List<Note> Get(Subject entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            }            
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        public override void Update(Note entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Notes.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public override void Delete(Note entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Notes.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
