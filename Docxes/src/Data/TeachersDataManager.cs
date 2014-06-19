using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Teachers.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Teacher> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<Teacher> Get(LocalDatabaseContainer container, Predicate<Teacher> predicate) {
            return (from
                        Teacher entity
                    in
                        container.Teachers
                            //.Include(entity => entity.Subjects
                            //    .Select(subject => subject.Documents)
                            //)
                            //.Include(entity => entity.Subjects
                            //    .Select(subject => subject.Notes)
                            //)
                            //.Include(entity => entity.Subjects
                            //    .Select(subject => subject.Grades)
                            //)
                            //.Include(entity => entity.Subjects
                            //    .Select(subject => subject.Events)
                            //)
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        internal override List<Teacher> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        internal override List<Teacher> Get(School entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.School.Equals(entitiesParent));
            }
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        internal override void Update(Teacher entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Teachers.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        internal override void Delete(Teacher entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Teachers.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
