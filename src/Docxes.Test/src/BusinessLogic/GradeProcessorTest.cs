using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VrankenBischof.Docxes;
using VrankenBischof.Docxes.BusinessLogic;
using System.Collections.Generic;

namespace VrankenBischof.Docxes.Test {

    [TestClass]
    public class GradeProcessorTest {

        GradeProcessor entityProcessor = new GradeProcessor();

        [TestInitialize]
        public void InitializeTests() {
            App.Initialize();
        }


        [TestMethod]
        public void CalculateAverageGrade_SameWeight() {
            // Arrange
            var weight = 64;
            var subject = new Subject();
            var grades = new List<Grade>() { new Grade(4.52M, weight, null, subject),
                                             new Grade(5.37M, weight, null, subject),
                                             new Grade(4.26M, weight, null, subject),
                                             new Grade(4.33M, weight, null, subject),
                                             new Grade(2.14M, weight, null, subject),
                                             new Grade(4.88M, weight, null, subject) };

            // Act
            var result = entityProcessor.CalculateAverageGrade(grades);

            // Assert
            Assert.AreEqual(4.25M, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateAverageGrade_NoGrades() {
            // Arrange
            var grades = new List<Grade>();

            // Act
            entityProcessor.CalculateAverageGrade(grades);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateAverageGrade_GradesNull() {
            // Act
            entityProcessor.CalculateAverageGrade(null);
        }

    }

}
