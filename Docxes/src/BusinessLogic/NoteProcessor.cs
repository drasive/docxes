using System;
using System.Collections.Generic;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides functionality to process notes.
    /// </summary>
    public sealed class NoteProcessor : BusinessObjectProcessor<Note, Subject> {

        /// <summary>
        /// Creates a new instance of the class <see cref="NoteProcessor"/>.
        /// </summary>
        public NoteProcessor() {
            dataManager = new NotesDataManager();
        }


        /// <summary>
        /// Indicates whether a new object can be saved.
        /// </summary>
        /// <param name="school">The parent of the object, on which the ability to save a new object is based on.</param>
        /// <returns>True if a new object can be saved; otherwise, false.</returns>
        public override bool CanCreate(IBusinessObject school) {
            if (school == null) {
                throw new ArgumentNullException("school");
            }
            // TODO:
            //if (school.GetType() != typeof(School)) {
            //    throw new ArgumentException("school is not of type \"School\"");
            //}
            var schoolAsSchool = (School)school;

            var subjectProcessor = new BusinessLogic.SubjectProcessor();
            return subjectProcessor.Get(schoolAsSchool).Count > 0;
        }

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public override void Create(Note objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public override List<Note> Get() {
            return dataManager.Get();
        }

        /// <summary>
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public override List<Note> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }


        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public override void Update(Note objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
        public override void Delete(Note objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
