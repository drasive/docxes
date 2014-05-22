using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class SchoolsDataManager : BusinessObjectDataManager<School> {

        public override void Create(School objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            var container = GetDatabaseContainer();
            container.Schools.Add(objectToSave);
            container.SaveChanges();
        }


        private List<School> Get(LocalDatabaseContainer container) {
            return (from School school
                    in container.Schools
                    select school).ToList();
        }

        public override List<School> Get() {
            var container = GetDatabaseContainer();
            return Get(container);
        }

        public override School Get(int id) {
            var container = GetDatabaseContainer();
            return Get(container).First(databaseElement => databaseElement.Id == id);
        }


        public override void Update(School objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Schools.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Schools.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(School objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            var container = GetDatabaseContainer();
            var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
            container.Schools.Remove(databaseObjectToDelete);
            container.SaveChanges();
        }

    }

}
