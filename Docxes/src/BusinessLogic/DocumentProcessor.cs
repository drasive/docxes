﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class DocumentProcessor : BusinessObjectProcessor<Document, Subject> {

        public DocumentProcessor() {
            dataManager = new DocumentsDataManager();
        }


        //public override bool CanCreate(Teacher objectParentParent) {
        //    var subjectProcessor = new BusinessLogic.SubjectProcessor();
        //    return subjectProcessor.Get(teacher).Count > 0;
        //}

        public override void Create(Document objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Document> Get( ) {
            return dataManager.Get();
        }

        public override List<Document> Get(Subject objectsParent) {
            return dataManager.Get(objectsParent);
        }


        public override void Update(Document objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(Document objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
