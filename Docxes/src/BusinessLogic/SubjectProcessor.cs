using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    // TODO: Make it possible to move to another teachers
    sealed class SubjectProcessor : BusinessObjectProcessor<Subject, Teacher> {

        public SubjectProcessor() {
            dataManager = new SubjectsDataManager();
        }


        //public override bool CanCreate() {
        //    // ASK Is this legit? If not, update all processors
        //    var teacherProcessor = new BusinessLogic.TeacherProcessor();
        //    return teacherProcessor.Get(ApplicationManager.Workspace.School).Count > 0;
        //}
        
        public override void Create(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Subject> Get(Teacher objectsParent) {
            return dataManager.Get(objectsParent);
        }


        public override void Update(Subject objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(Subject objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            // Delete dependencies
            var documentProcessor = new DocumentProcessor();
            foreach (Document dependencyToDelete in objectToDelete.Documents) {
                documentProcessor.Delete(dependencyToDelete);
            }

            var noteProcessor = new NoteProcessor();
            foreach (Note dependencyToDelete in objectToDelete.Notes) {
                noteProcessor.Delete(dependencyToDelete);
            }

            var gradeProcessor = new GradeProcessor();
            foreach (Grade dependencyToDelete in objectToDelete.Grades) {
                gradeProcessor.Delete(dependencyToDelete);
            }

            var eventProcessor = new EventProcessor();
            foreach (Event dependencyToDelete in objectToDelete.Events) {
                eventProcessor.Delete(dependencyToDelete);
            }

            // Delete object
            dataManager.Delete(objectToDelete);
        }

    }

}
