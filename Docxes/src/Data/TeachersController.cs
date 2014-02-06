using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class TeachersController : ManagementElementController<Teacher> {

        public override void Save(Teacher elementToSave) {
            if (elementToSave == null) {
                throw new ArgumentNullException("elementToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Teachers.Add(elementToSave);
                container.SaveChanges();
            }
        }


        public override IEnumerable<Teacher> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return container.Set<Teacher>();    
            }
        }

        public override Teacher Get(int id) {
            return new Teacher();
        }


        public override void Update(Teacher elementToUpdate) {
            if (elementToUpdate == null) {
                throw new ArgumentNullException("elementToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToUpdate = container.Teachers.First(databaseElement => databaseElement.Id == elementToUpdate.Id);
                databaseElementToUpdate = elementToUpdate;
                container.SaveChanges();
            }
        }


        public override void Delete(Teacher elementToDelete) {
            if (elementToDelete == null) {
                throw new ArgumentNullException("elementToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseElementToDelete = container.Teachers.First(databaseElement => databaseElement.Id == elementToDelete.Id);
                container.Teachers.Remove(databaseElementToDelete);
                container.SaveChanges();
            }
        }

    }

}
