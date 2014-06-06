using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage subjects in nonvolatile memory.
    /// </summary>
    public sealed class SubjectsDataManager : BusinessObjectDataManager<Subject, Teacher> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        public override void Create(Subject entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Subjects.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Subject> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<Subject> Get(LocalDatabaseContainer container, Predicate<Subject> predicate) {
            return (from
                        Subject entity
                    in
                        container.Subjects
                            .Include(entity => entity.Teacher)

                            .Include(entity => entity.Documents)
                            .Include(entity => entity.Notes)
                            .Include(entity => entity.Grades)
                            .Include(entity => entity.Events)
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public override List<Subject> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        public override List<Subject> Get(Teacher entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Teacher.Equals(entitiesParent));
            }
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        public override void Update(Subject entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Subjects.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public override void Delete(Subject entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Subjects.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
