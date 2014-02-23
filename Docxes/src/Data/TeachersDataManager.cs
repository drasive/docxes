using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class TeachersDataManager : BusinessObjectDataManager<Teacher> {

        public override void Insert(Teacher objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Teachers.Add(objectToSave);
                container.SaveChanges();
            }
        }


        public override IEnumerable<Teacher> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // TODO: Paste from School
                return container.Set<Teacher>();    
            }
        }

        public override Teacher Get(int id) {
            return new Teacher();
        }


        public override void Update(Teacher objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToUpdate = container.Teachers.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                databaseObjectToUpdate = objectToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(Teacher objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToDelete = container.Teachers.First(databaseElement => databaseElement.Id == objectToDelete.Id);
                container.Teachers.Remove(databaseObjectToDelete);
                container.SaveChanges();
            }
        }

    }

}
