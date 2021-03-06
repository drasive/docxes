﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage notes in nonvolatile memory.
    /// </summary>
    internal sealed class NotesDataManager : BusinessObjectDataManager<Note, Subject> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        internal override void Create(Note entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            databaseContainer.Notes.Add(entityToSave);

            databaseContainer.SaveChanges();
        }


        private List<Note> Get(Predicate<Note> predicate) {
            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            return (from
                        Note entity
                    in
                        databaseContainer.Notes
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
        internal override List<Note> Get() {
            return Get(entity => true);
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        internal override List<Note> Get(Subject entitiesParent) {
            if (entitiesParent == null) {
                throw new ArgumentNullException("entitiesParent");
            }

            return Get(entity => entity.Subject.Equals(entitiesParent));
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        internal override void Update(Note entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToUpdate = databaseContainer.Notes.Find(entityToUpdate.Id);
            databaseContainer.Entry(databaseObjectToUpdate).CurrentValues.SetValues(entityToUpdate);

            databaseContainer.SaveChanges();
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        internal override void Delete(Note entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToDelete = databaseContainer.Notes.Find(entityToDelete.Id);
            databaseContainer.Notes.Remove(databaseObjectToDelete);

            databaseContainer.SaveChanges();
        }

    }

}
