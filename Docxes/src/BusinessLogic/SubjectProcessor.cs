using System;
using System.Collections.Generic;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides functionality to process subjects.
    /// </summary>
    public sealed class SubjectProcessor : BusinessObjectProcessor<Subject, Teacher> {

        /// <summary>
        /// Creates a new instance of the class <see cref="SubjectProcessor"/>.
        /// </summary>
        public SubjectProcessor() {
            dataManager = new SubjectsDataManager();
        }


        //public override bool CanCreate() {
        //    // ASK Is this legit? If not, update all processors
        //    var teacherProcessor = new BusinessLogic.TeacherProcessor();
        //    return teacherProcessor.Get(ApplicationManager.Workspace.School).Count > 0;
        //}

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public override void Create(Subject objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public override List<Subject> Get() {
            return dataManager.Get();
        }

        /// <summary>
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public override List<Subject> Get(Teacher objectsParent) {
            return dataManager.Get(objectsParent);
        }


        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public override void Update(Subject objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
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
