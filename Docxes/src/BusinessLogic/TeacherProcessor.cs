﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class TeacherProcessor : BusinessObjectProcessor<Teacher, School> {

        public TeacherProcessor() {
            dataManager = new TeachersDataManager();
        }


        //public override bool CanCreate() {
        //    // TODO: Only if schools exist
        //    return true;
        //}

        public override void Create(Teacher objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Teacher> Get(School objectsParent) {
            return dataManager.Get(objectsParent);
        }


        public override void Update(Teacher objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(Teacher objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            // Delete dependencies
            var subjectProcessor = new SubjectProcessor();
            foreach (Subject dependencyToDelete in objectToDelete.Subjects) {
                subjectProcessor.Delete(dependencyToDelete);
            }

            // Delete object
            dataManager.Delete(objectToDelete);
        }

    }

}
