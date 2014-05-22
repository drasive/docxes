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

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Notes.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Note> Get(LocalDatabaseContainer container) {
            return (from
                        Note note
                    in
                        container.Notes
                    select
                        note
                    ).ToList();
        }

        public override List<Note> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override Note Get(int id) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Note objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
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

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Notes.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
