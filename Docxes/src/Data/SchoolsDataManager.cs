using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class SchoolsDataManager : BusinessObjectDataManager<School> {

        public override void Create(School objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Schools.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<School> Get(LocalDatabaseContainer container) {
            return (from
                        School school
                    in
                        container.Schools
                            .Include(school => school.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Documents)
                                )
                            )
                            .Include(school => school.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Notes)
                                )
                            )
                            .Include(school => school.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Grades)
                                )
                            )
                            .Include(school => school.Teachers
                                .Select(teacher => teacher.Subjects
                                    .Select(subject => subject.Events)
                                )
                            )
                    select
                        school
                    ).ToList();
        }

        public override List<School> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }


        public override void Update(School objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
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


        public override void Delete(School objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Schools.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
