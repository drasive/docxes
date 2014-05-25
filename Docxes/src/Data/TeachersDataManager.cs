using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class TeachersDataManager : BusinessObjectDataManager<Teacher, School> {

        public override void Create(Teacher entityToSave) {
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
                            .Include(entity => entity.Subjects
                                .Select(subject => subject.Documents)
                            )
                            .Include(entity => entity.Subjects
                                .Select(subject => subject.Notes)
                            )
                            .Include(entity => entity.Subjects
                                .Select(subject => subject.Grades)
                            )
                            .Include(entity => entity.Subjects
                                .Select(subject => subject.Events)
                            )
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        public override List<Teacher> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override List<Teacher> Get(School entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.School.Equals(entitiesParent));
            }
        }


        public override void Update(Teacher entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Teachers.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        public override void Delete(Teacher entityToDelete) {
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
