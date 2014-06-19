using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VrankenBischof.Docxes;
using VrankenBischof.Docxes.BusinessLogic;

namespace DocxesTesting {

    [TestClass]
    public class SchoolProcessorTest {

        private static string GetRandomEntityName() {
            return "[AUTOMATED TESTING] " + new Guid().GetHashCode();
        }

        [TestMethod]
        public void TestMethod1() {
            // TODO: __DV

            // Arrange
            var SchoolProcessor = new SchoolProcessor();

            // Act & Assert
            var schoolToSave = new School(GetRandomEntityName(), "Test comment");
            SchoolProcessor.Create(schoolToSave);

            //Update

            //Delete
        }

    }

}
