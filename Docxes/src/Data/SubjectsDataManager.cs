using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class SubjectsDataManager : BusinessObjectDataManager<Subject, Teacher> {

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

        public override List<Subject> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override List<Subject> Get(Teacher entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Teacher.Equals(entitiesParent));
            }
        }


        public override void Update(Subject entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Subjects.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Subjects.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


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
