using System;
using System.Collections.Generic;
using System.Linq;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides functionality to manage documents in nonvolatile memory.
    /// </summary>
    internal sealed class DocumentsDataManager : BusinessObjectDataManager<Document, Subject> {

        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        internal override void Create(Document entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();
            //using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Documents.Add(entityToSave);
                databaseContainer.SaveChanges();
            //}
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

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        internal override List<Document> Get() {
            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer(); 
            //using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            //}
        }

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        internal override List<Document> Get(Subject entitiesParent) {
            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();
            //using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            //}
        }


        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        internal override void Update(Document entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            // TODO: __DV

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();
            //using (var databaseContainer = GetDatabaseContainer()) {
                //databaseContainer.Documents.Attach(entityToUpdate);
                //databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                //databaseContainer.SaveChanges();



                //var a = databaseContainer.Documents.Find(entityToUpdate.Id);
                //a = entityToUpdate;
                //
                //
                //databaseContainer.Entry(a).State = System.Data.Entity.EntityState.Modified;
                //databaseContainer.SaveChanges();



                var a = databaseContainer.Documents.Find(entityToUpdate.Id);
                databaseContainer.Entry(a).CurrentValues.SetValues(entityToUpdate);

                databaseContainer.SaveChanges();
            //}
        }


        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        internal override void Delete(Document entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            var databaseContainer = DatabaseContainerManager.GetLocalDatabaseContainer();
            //using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Documents.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            //}
        }

    }

}
