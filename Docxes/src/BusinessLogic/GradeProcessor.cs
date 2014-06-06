﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides functionality to process grades.
    /// </summary>
    public sealed class GradeProcessor : BusinessObjectProcessor<Grade, Subject> {

        /// <summary>
        /// Creates a new instance of the class <see cref="GradeProcessor"/>.
        /// </summary>
        public GradeProcessor() {
            dataManager = new GradesDataManager();
        }


        //public override bool CanCreate(Teacher teacher) {
        //    var subjectProcessor = new BusinessLogic.SubjectProcessor();
        //    return subjectProcessor.Get(teacher).Count > 0;
        //}

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public override void Create(Grade objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public override List<Grade> Get() {
            return dataManager.Get();
        }

        /// <summary>
        /// Gets all existing business objects with the provided parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the provided parent.</returns>
        public override List<Grade> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }

        public List<Grade> Get(Event objectsParent) {
            return ((GradesDataManager)dataManager).Get(objectsParent);
        }


        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public override void Update(Grade objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
        public override void Delete(Grade objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
