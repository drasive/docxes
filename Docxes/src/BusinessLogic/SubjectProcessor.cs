using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class SubjectProcessor : BusinessObjectProcessor<Subject> {

        public SubjectProcessor() {
            dataManager = new SubjectsDataManager();
        }


        public override bool CanCreate() {
            // ASK Is this legit? If not, update in all Processors
            var teacherProcessor = new BusinessLogic.TeacherProcessor();
            return teacherProcessor.Get().Count > 0;
        }
        
        public override void Create(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Subject> Get() {
            return dataManager.Get();
        }

        public override Subject Get(int id) {
            return dataManager.Get(id);
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
