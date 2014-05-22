using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    abstract class BusinessObjectProcessor<T> where T : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<T> dataManager;

        public abstract bool AreRequirementsMetToCreate();
        public abstract void Create(T businessObjectToSave);

        public abstract List<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T businessObjectToUpdate);

        public abstract void Delete(T businessObjectToDelete);

    }

}
