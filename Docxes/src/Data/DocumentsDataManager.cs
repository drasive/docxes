using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class DocumentsDataManager : BusinessObjectDataManager<Document> {

        public override void Create(Document objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                container.Documents.Add(objectToSave);
                container.SaveChanges();
            }
        }


        private List<Document> Get(LocalDatabaseContainer container) {
            return (from Document Document
                    in container.Documents
                    select Document).ToList();
        }

        public override List<Document> Get() {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return Get(container);
            }
        }

        public override Document Get(int id) {
            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                return Get(container).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Document objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                // REFACTOR: Replace with Get(container)
                // BUG

                //var databaseObjectToUpdate = container.Documents.First(databaseElement => databaseElement.Id == objectToUpdate.Id);
                //databaseObjectToUpdate = objectToUpdate;

                //container.Documents.Attach(objectToUpdate);
                //container.Entry(objectToUpdate).State = System.Data.Entity.EntityState.Modified;
                //container.SaveChanges();
            }
        }


        public override void Delete(Document objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            using (LocalDatabaseContainer container = new LocalDatabaseContainer()) {
                var databaseObjectToDelete = Get(container).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                container.Documents.Remove(databaseObjectToDelete);
                container.SaveChanges();
            }
        }

    }

}
