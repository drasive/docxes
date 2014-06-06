using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage schools in nonvolatile memory.
    /// </summary>
    public sealed class SchoolsDataManager : BusinessObjectDataManager<School> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        public override void Create(School entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Schools.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<School> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<School> Get(LocalDatabaseContainer container, Func<School, bool> predicate) {
            return (from
                        School entity
                    in
                        container.Schools
                            .Include(entity => entity.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Documents)
                                )
                            )
                            .Include(entity => entity.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Notes)
                                )
                            )
                            .Include(entity => entity.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Grades)
                                )
                            )
                            .Include(entity => entity.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Events)
                                )
                            )
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public override List<School> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        public override void Update(School entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Schools.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public override void Delete(School entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Schools.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
