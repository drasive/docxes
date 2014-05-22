using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class SubjectsDataManager : BusinessObjectDataManager<Subject> {

        public override void Create(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            var container = GetDatabaseContainer();
            container.Subjects.Add(objectToSave);
            container.SaveChanges();
        }


        private List<Subject> Get(LocalDatabaseContainer container) {
            return (from Subject Subject
                    in container.Subjects
                    select Subject).ToList();
        }

        public override List<Subject> Get() {
            var container = GetDatabaseContainer();
            return Get(container);
        }

        public override Subject Get(int id) {
            var container = GetDatabaseContainer();
            return Get(container).First(databaseElement => databaseElement.Id == id);
        }


        public override void Update(Subject objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Subjects.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Subjects.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Subject objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            var container = GetDatabaseContainer();
            var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
            container.Subjects.Remove(databaseObjectToDelete);
            container.SaveChanges();
        }

    }

}
