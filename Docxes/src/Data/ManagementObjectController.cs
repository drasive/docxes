using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    abstract class ManagementObjectController<T> where T: Docxes.IManagementObject {
        
        public abstract void Save(T objectToSave);

        public abstract IEnumerable<T> Get();
        public abstract T Get(int id);

        public abstract void Update(T objectToUpdate);

        public abstract void Delete(T objectToDelete);

    }

}
