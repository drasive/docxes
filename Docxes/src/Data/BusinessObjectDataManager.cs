using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    abstract class BusinessObjectDataManager<T> where T: Docxes.IBusinessObject {
        
        public abstract void Insert(T businessObjectToSave);

        public abstract IEnumerable<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T businessObjectToUpdate);

        public abstract void Delete(T businessObjectToDelete);

    }

}
