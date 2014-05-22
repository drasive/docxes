using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    sealed class NoteProcessor : BusinessObjectProcessor<Note> {

        public NoteProcessor() {
            dataManager = new NotesDataManager();
        }


        public override bool AreRequirementsMetToCreate() {
            return true;
        }

        public override void Create(Note objectToSave) {
            if (objectToSave == null) {
                throw new ArgumentNullException("objectToSave");
            }

            dataManager.Create(objectToSave);
        }


        public override List<Note> Get() {
            return dataManager.Get();
        }

        public override Note Get(int id) {
            return dataManager.Get(id);
        }


        public override void Update(Note objectToUpdate) {
            if (objectToUpdate == null) {
                throw new ArgumentNullException("objectToUpdate");
            }

            dataManager.Update(objectToUpdate);
        }


        public override void Delete(Note objectToDelete) {
            if (objectToDelete == null) {
                throw new ArgumentNullException("objectToDelete");
            }

            dataManager.Delete(objectToDelete);
        }

    }

}
