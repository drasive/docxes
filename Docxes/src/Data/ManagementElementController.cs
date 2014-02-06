using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    abstract class ManagementElementController<T> where T: Docxes.IManagementElement {
        
        public abstract void Save(T managementElementToSave);

        public abstract IEnumerable<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T managementElementToUpdate);

        public abstract void Delete(T managementElementToDelete);

    }

}
