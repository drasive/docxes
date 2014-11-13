using System;
using System.Collections.Generic;
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

            var subjectProcessor = new BusinessLogic.SubjectProcessor();
            return subjectProcessor.Get(schoolAsSchool).Count > 0;
        }

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
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public override List<Grade> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }

        /// <summary>
        /// Gets all existing business objects for the specified school.
        /// </summary>
        /// <param name="school">The school that the returned business objects must belong to.</param>
        /// <returns>A list of all existing business objects for the specified school.</returns>
        public List<Grade> Get(School school) {
            var subjectProcessor = new BusinessLogic.SubjectProcessor();
            var subjects = subjectProcessor.Get(school);

            List<Grade> grades = new List<Grade>();
            foreach (var subject in subjects) {
                grades.AddRange(dataManager.Get(subject));
            }

            return grades;
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


        /// <summary>
        /// Calculates the average of a list grades.
        /// </summary>
        /// <param name="grades">The grades to calculate the average of.</param>
        /// <returns>The average of the specified grades.</returns>
        /// <remarks>Thanks to Dominik Fringeli and Thomas Frey for providing us with this algorithm!</remarks>
        public decimal CalculateAverageGrade(List<Grade> grades) {
            if (grades == null) {
                throw new ArgumentNullException("grades");
            }
            if (grades.Count == 0) {
                throw new ArgumentException("\"grades\" does not contain any elements");
            }

            decimal totalValue = 0;
            decimal denominator = 0;
            foreach (var grade in grades) {
                totalValue += grade.Value * (grade.Weight / 100M);
                denominator += (grade.Weight / 100M);
            }

            var average = totalValue / denominator;
            return average;
        }

        /// <summary>
        /// Calculates the required grade to archieve a certain average, taking existing grades into account.
        /// </summary>
        /// <param name="existingGrades">The existing grades that contribute to the average.</param>
        /// <param name="targetGrade">The grade that wants to be archieved.</param>
        /// <returns>
        /// The required grade to reach the specified target, taking the existing grades into account.
        /// Null if the specified target can't be reached with a single additional grade.
        /// </returns>
        public decimal? CalculateRequiredGrade(List<Grade> existingGrades, decimal targetGrade) {
            if (existingGrades == null) {
                throw new ArgumentNullException("existingGrades");
            }

            if (existingGrades.Count == 0) {
                return targetGrade;
            }

            decimal totalValue = 0;
            decimal denominator = 0;
            foreach (var grade in existingGrades) {
                totalValue += grade.Value * (grade.Weight / 100M);
                denominator += (grade.Weight / 100M);
            }

            var requiredGrade = (denominator + 1) * targetGrade - totalValue;
            if (requiredGrade >= 1 && requiredGrade <= 6) {
                return requiredGrade;
            }
            else {
                return null;
            }
        }

    }

}
