using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class SubjectsDataManager : BusinessObjectDataManager<Subject> {

        public override void Insert(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Subjects.Add(objectToSave);
                container.SaveChanges();
            }
        }


        public override IEnumerable<Subject> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // TODO: Paste from School
                return container.Set<Subject>();
            }
        }

        public override Subject Get(int id) {
            return new Subject();
        }


        public override void Update(Subject objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToUpdate = container.Subjects.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                databaseObjectToUpdate = objectToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(Subject objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToDelete = container.Subjects.First(databaseElement => databaseElement.Id == objectToDelete.Id);
                container.Subjects.Remove(databaseObjectToDelete);
                container.SaveChanges();
            }
        }

    }

}
