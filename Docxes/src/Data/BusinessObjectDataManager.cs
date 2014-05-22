using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    abstract class BusinessObjectDataManager<T> where T: Docxes.IBusinessObject {

        protected LocalDatabaseContainer GetDatabaseContainer() {
            return new LocalDatabaseContainer();
        }


        public abstract void Create(T businessObjectToSave);

        public abstract List<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T businessObjectToUpdate);

        public abstract void Delete(T businessObjectToDelete);

    }

}
