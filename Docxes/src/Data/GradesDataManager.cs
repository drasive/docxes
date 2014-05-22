using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class GradesDataManager : BusinessObjectDataManager<Grade> {

        public override void Create(Grade objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Grades.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Grade> Get(LocalDatabaseContainer container) {
            return (from Grade grade
                    in container.Grades
                    select grade).ToList();
        }

        public override List<Grade> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override Grade Get(int id) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Grade objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Grades.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Grades.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Grade objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Grades.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
