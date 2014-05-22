using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class SubjectsDataManager : BusinessObjectDataManager<Subject> {

        public override void Create(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Subjects.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Subject> Get(LocalDatabaseContainer container) {
            return (from
                        Subject subject
                    in
                        container.Subjects
                            .Include(businessObject => businessObject.Documents)
                            .Include(businessObject => businessObject.Notes)
                            .Include(businessObject => businessObject.Grades)
                            .Include(businessObject => businessObject.Events)
                    select
                        subject
                    ).ToList();
        }

        public override List<Subject> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override Subject Get(int id) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Subject objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
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


        public override void Delete(Subject objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Subjects.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
