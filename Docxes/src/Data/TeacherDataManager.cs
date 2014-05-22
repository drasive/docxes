using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class TeachersDataManager : BusinessObjectDataManager<Teacher> {

        public override void Create(Teacher objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            var container = GetDatabaseContainer();
            container.Teachers.Add(objectToSave);
            container.SaveChanges();
        }


        private List<Teacher> Get(LocalDatabaseContainer container) {
            return (from Teacher Teacher
                    in container.Teachers
                    select Teacher).ToList();
        }

        public override List<Teacher> Get() {
            var container = GetDatabaseContainer();
            return Get(container);
        }

        public override Teacher Get(int id) {
            // TODO: Use a functional style condition as the parameter in all of these classes

            var container = GetDatabaseContainer();
            return Get(container).First(databaseElement => databaseElement.Id == id);
        }


        public override void Update(Teacher objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Teachers.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Teachers.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Teacher objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            var container = GetDatabaseContainer();
            var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
            container.Teachers.Remove(databaseObjectToDelete);
            container.SaveChanges();
        }

    }

}
