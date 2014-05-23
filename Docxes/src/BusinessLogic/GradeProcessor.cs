using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class GradeProcessor : BusinessObjectProcessor<Grade, Subject> {

        public GradeProcessor() {
            dataManager = new GradesDataManager();
        }


        //public override bool CanCreate(Teacher teacher) {
        //    var subjectProcessor = new BusinessLogic.SubjectProcessor();
        //    return subjectProcessor.Get(teacher).Count > 0;
        //}

        public override void Create(Grade objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Grade> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }


        public override void Update(Grade objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(Grade objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
