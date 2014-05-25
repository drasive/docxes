using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class DocumentsDataManager : BusinessObjectDataManager<Document, Subject> {

        public override void Create(Document entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Documents.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Document> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<Document> Get(LocalDatabaseContainer databaseContainer, Predicate<Document> predicate) {
            return (from
                        Document entity
                    in
                        databaseContainer.Documents
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        public override List<Document> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override List<Document> Get(Subject entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            }
        }


        public override void Update(Document entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Documents.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        public override void Delete(Document entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Documents.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
