﻿using System;
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


        /// <summary>
        /// Indicates whether a new object can be saved.
        /// </summary>
        /// <param name="school">The parent of the object, on which the ability to save a new object is based on.</param>
        /// <returns>True if a new object can be saved; otherwise, false.</returns>
        public override bool CanCreate(IBusinessObject school) {
            if (school == null) {
                throw new ArgumentNullException("school");
            }

            School schoolAsSchool;
            try {
                schoolAsSchool = (School)school;
            }
            catch {
                throw new ArgumentException("parameter \"school\" cannot be converted to type \"School\"");
            }

            var teacherProcessor = new BusinessLogic.TeacherProcessor();
            return teacherProcessor.Get(schoolAsSchool).Count > 0;
        }

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
        /// Gets all existing business objects for the specified school.
        /// </summary>
        /// <param name="school">The school that the returned business objects must belong to.</param>
        /// <returns>A list of all existing business objects for the specified school.</returns>
        public List<Subject> Get(School school) {
            var teacherProcessor = new BusinessLogic.TeacherProcessor();
            var teachers = teacherProcessor.Get(school);

            List<Subject> subjects = new List<Subject>();
            foreach (var teacher in teachers) {
                subjects.AddRange(dataManager.Get(teacher));
            }

            return subjects;
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

            // Delete object
            dataManager.Delete(objectToDelete);
        }

    }

}
