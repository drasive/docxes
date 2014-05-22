using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class NotesDataManager : BusinessObjectDataManager<Note> {

        public override void Create(Note objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            var container = GetDatabaseContainer();
            container.Notes.Add(objectToSave);
            container.SaveChanges();
        }


        private List<Note> Get(LocalDatabaseContainer container) {
            return (from Note Note
                    in container.Notes
                    select Note).ToList();
        }

        public override List<Note> Get() {
            var container = GetDatabaseContainer();
            return Get(container);
        }

        public override Note Get(int id) {
            var container = GetDatabaseContainer();
            return Get(container).First(databaseElement => databaseElement.Id == id);
        }


        public override void Update(Note objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Notes.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Notes.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Note objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            var container = GetDatabaseContainer();
            var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
            container.Notes.Remove(databaseObjectToDelete);
            container.SaveChanges();
        }

    }

}
