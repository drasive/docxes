using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class SubjectsController : ManagementElementController<Subject> {

        public override void Save(Subject elementToSave) {
            if (elementToSave == null) {
                throw new ArgumentNullException("elementToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Subjects.Add(elementToSave);
                container.SaveChanges();
            }
        }


        public override IEnumerable<Subject> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return container.Set<Subject>();    
            }
        }

        public override Subject Get(int id) {
            return new Subject();
        }


        public override void Update(Subject elementToUpdate) {
            if (elementToUpdate == null) {
                throw new ArgumentNullException("elementToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToUpdate = container.Subjects.First(databaseElement => databaseElement.Id == elementToUpdate.Id);
                databaseElementToUpdate = elementToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(Subject elementToDelete) {
            if (elementToDelete == null) {
                throw new ArgumentNullException("elementToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToDelete = container.Subjects.First(databaseElement => databaseElement.Id == elementToDelete.Id);
                container.Subjects.Remove(databaseElementToDelete);
                container.SaveChanges();
            }
        }

    }

}
