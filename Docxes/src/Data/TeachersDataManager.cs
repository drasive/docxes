using System;
using System.Collections.Generic;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage teachers in nonvolatile memory.
    /// </summary>
    internal sealed class TeachersDataManager : BusinessObjectDataManager<Teacher, School> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        internal override void Create(Teacher entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            databaseContainer.Teachers.Add(entityToSave);

            databaseContainer.SaveChanges();
        }


        private List<Teacher> Get(Predicate<Teacher> predicate) {
            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            return (from
                        Teacher entity
                    in
                        databaseContainer.Teachers
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        internal override List<Teacher> Get() {
            return Get(entity => true);
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        internal override List<Teacher> Get(School entitiesParent) {
            if (entitiesParent == null) {
                throw new ArgumentNullException("entitiesParent");
            }

            return Get(entity => entity.School.Equals(entitiesParent));
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        internal override void Update(Teacher entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToUpdate = databaseContainer.Teachers.Find(entityToUpdate.Id);
            databaseContainer.Entry(databaseObjectToUpdate).CurrentValues.SetValues(entityToUpdate);

            databaseContainer.SaveChanges();
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        internal override void Delete(Teacher entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();

            var databaseObjectToDelete = databaseContainer.Teachers.Find(entityToDelete.Id);
            databaseContainer.Teachers.Remove(databaseObjectToDelete);

            databaseContainer.SaveChanges();
        }

    }

}
