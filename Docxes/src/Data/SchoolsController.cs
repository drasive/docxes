using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    // TODO: Fix
    sealed class SchoolsController : ManagementObjectController<School> {

        public override void Save(School objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Schools.Add(objectToSave);
                container.SaveChanges();
            }
        }


        public override IEnumerable<School> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return container.Set<School>();    
            }
        }

        public override School Get(int id) {
            return new School();
        }


        public override void Update(School objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var objectToUpdateInDatabase = container.Schools.First(i => i.Id == objectToUpdate.Id);
                objectToUpdateInDatabase = objectToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(School objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var objectToDeleteInDatabase = container.Schools.First(i => i.Id == objectToDelete.Id);
                container.Schools.Remove(objectToDeleteInDatabase);
                container.SaveChanges();
            }
        }

    }

}
