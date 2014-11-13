using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VrankenBischof.Docxes;
using VrankenBischof.Docxes.BusinessLogic;

namespace VrankenBischof.Docxes.Test {

    [TestClass]
    public class SchoolProcessorTest {

        private static string GetRandomEntityName() {
            return Guid.NewGuid().ToString().Substring(0, 32);
        }

        [TestInitialize]
        public void InitializeTests() {
            App.Initialize();
        }


        [TestMethod]
        public void DatabaseOperations() {
            // Arrange
            var entityProcessor = new SchoolProcessor();
            var initialEntityCount = entityProcessor.Get().Count;

            // Act & Assert
            // -- Insert
            var initialEntityName = GetRandomEntityName();
            var initialEntityComment = "Comment 1";
            var initialEntity = new School(initialEntityName, initialEntityComment);

            entityProcessor.Create(initialEntity);

            Assert.AreEqual(initialEntityCount + 1, entityProcessor.Get().Count);

            // -- Update
            var updatedEntityName = GetRandomEntityName();
            var updatedEntityComment = "Comment 2";
            var updatedEntity = new School(updatedEntityName, updatedEntityComment, initialEntity);

            entityProcessor.Update(updatedEntity);

            Assert.AreEqual(initialEntityCount + 1, entityProcessor.Get().Count);

            // -- Delete
            entityProcessor.Delete(initialEntity);

            Assert.AreEqual(initialEntityCount, entityProcessor.Get().Count);
        }

    }

}
