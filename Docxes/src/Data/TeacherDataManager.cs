using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace VrankenBischof.Docxes.Data {

    sealed class TeachersDataManager : BusinessObjectDataManager<Teacher> {

        public override void Create(Teacher objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
                databaseContainer.Teachers.Add(objectToSave);
                databaseContainer.SaveChanges();
            }
        }


        private List<Teacher> Get(LocalDatabaseContainer container) {
            return (from
                        Teacher teacher
                    in
                        container.Teachers
                            .Include(businessObject => businessObject.Subjects)
                    select
                        teacher
                    ).ToList();
        }

        public override List<Teacher> Get() {
            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer);
            }
        }

        public override Teacher Get(int id) {
            // TODO: Use a functional style condition as the parameter in all of these classes

            using (var databaseContainer = GetDatabaseContainer()) {
                return Get(databaseContainer).First(databaseElement => databaseElement.Id == id);
            }
        }


        public override void Update(Teacher objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            using (var databaseContainer = GetDatabaseContainer()) {
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

            using (var databaseContainer = GetDatabaseContainer()) {
                var databaseObjectToDelete = Get(databaseContainer).First(databaseElement => databaseElement.Id == objectToDelete.Id);
                databaseContainer.Teachers.Remove(databaseObjectToDelete);
                databaseContainer.SaveChanges();
            }
        }

    }

}
