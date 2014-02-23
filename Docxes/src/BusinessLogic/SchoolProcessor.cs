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


        public override void Save(School objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Save(objectToSave);
        }


        public override IEnumerable<School> Get() {
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

            dataManager.Delete(objectToDelete);
        }

    }

}
