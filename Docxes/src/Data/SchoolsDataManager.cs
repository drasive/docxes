using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class SchoolsDataManager : BusinessObjectDataManager<School> {

        public override void Create(School entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Schools.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
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

        public override List<School> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => true);
            }
        }


        public override void Update(School entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Schools.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Schools.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


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
