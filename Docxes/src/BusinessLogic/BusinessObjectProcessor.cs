using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    abstract class BusinessObjectProcessor<T> where T : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<T> dataManager;

        // TODO: Rename to Insert?
        // ASK: Call it "Save" and merge it with "Update"?
        public abstract void Save(T businessObjectToSave);

        public abstract IEnumerable<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T businessObjectToUpdate);

        public abstract void Delete(T businessObjectToDelete);

    }

}
