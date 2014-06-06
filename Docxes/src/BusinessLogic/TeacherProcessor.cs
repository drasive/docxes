﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides functionality to process teachers.
    /// </summary>
    public sealed class TeacherProcessor : BusinessObjectProcessor<Teacher, School> {

        /// <summary>
        /// Creates a new instance of the class <see cref="TeacherProcessor"/>.
        /// </summary>
        public TeacherProcessor() {
            dataManager = new TeachersDataManager();
        }


        //public override bool CanCreate() {
        //    // TODO: Only if schools exist
        //    return true;
        //}

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public override void Create(Teacher objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public override List<Teacher> Get() {
            return dataManager.Get();
        }

        /// <summary>
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public override List<Teacher> Get(School objectsParent) {
            return dataManager.Get(objectsParent);
        }


        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public override void Update(Teacher objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
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
