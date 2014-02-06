using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    // TODO: Fix database access
    sealed class SchoolsController : ManagementElementController<School> {

        public override void Save(School elementToSave) {
            if (elementToSave == null) {
                throw new ArgumentNullException("elementToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Schools.Add(elementToSave);
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


        public override void Update(School elementToUpdate) {
            if (elementToUpdate == null) {
                throw new ArgumentNullException("elementToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToUpdate = container.Schools.First(databaseElement => databaseElement.Id == elementToUpdate.Id);
                databaseElementToUpdate = elementToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(School elementToDelete) {
            if (elementToDelete == null) {
                throw new ArgumentNullException("elementToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToDelete = container.Schools.First(databaseElement => databaseElement.Id == elementToDelete.Id);
                container.Schools.Remove(databaseElementToDelete);
                container.SaveChanges();
            }
        }

    }

}
