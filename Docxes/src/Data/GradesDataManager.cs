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

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Grades.Add(objectToSave);
                container.SaveChanges();
            }
        }


        private List<Grade> Get(LocalDatabaseContainer container) {
            return (from Grade grade
                    in container.Grades
                    select grade).ToList();
        }

        public override List<Grade> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return Get(container);
            }
        }

        public override Grade Get(int id) {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return Get(container).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Grade objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
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

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                container.Grades.Remove(databaseObjectToDelete);
                container.SaveChanges();
            }
        }

    }

}
