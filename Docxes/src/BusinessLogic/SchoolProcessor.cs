using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class SchoolProcessor : BusinessObjectProcessor<School> {

        public SchoolProcessor() {
            dataManager = new SchoolsDataManager();
        }


        public override bool CanCreate() {
            return true;
        }

        public override void Create(School objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<School> Get() {
            return dataManager.Get();
        }

        public override School Get(int id) {
            return dataManager.Get(id);
        }


        public override void Update(School objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(School objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            // Delete dependencies
            var teacherProcessor = new TeacherProcessor();
            foreach (Teacher dependencyToDelete in objectToDelete.Teachers) {
                teacherProcessor.Delete(dependencyToDelete);
            }

            // Delete object
            dataManager.Delete(objectToDelete);
        }

    }

}
