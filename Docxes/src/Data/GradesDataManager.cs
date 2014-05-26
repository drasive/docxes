using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    sealed class GradesDataManager : BusinessObjectDataManager<Grade, Subject> {

        public override void Create(Grade entityToSave) {
            if (entityToSave == null) {
                throw new ArgumentNullException("entityToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Grades.Add(entityToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Grade> Get(LocalDatabaseContainer databaseContainer) {
            return Get(databaseContainer, entity => true);
        }

        private List<Grade> Get(LocalDatabaseContainer databaseContainer, Predicate<Grade> predicate) {
            return (from
                        Grade entity
                    in
                        databaseContainer.Grades
                    select
                        entity
                    ).ToList().Where(entity => predicate(entity)).ToList();
        }

        public override List<Grade> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override List<Grade> Get(Subject entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Subject.Equals(entitiesParent));
            }
        }

        public List<Grade> Get(Event entitiesParent) {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer, entity => entity.Event.Equals(entitiesParent));
            }
        }


        public override void Update(Grade entityToUpdate) {
            if (entityToUpdate == null) {
                throw new ArgumentNullException("entityToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Grades.Attach(entityToUpdate);
                databaseContainer.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                databaseContainer.SaveChanges();
            }
        }


        public override void Delete(Grade entityToDelete) {
            if (entityToDelete == null) {
                throw new ArgumentNullException("entityToDelete");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer, entity => entity.Id == entityToDelete.Id).First();
                databaseContainer.Grades.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
