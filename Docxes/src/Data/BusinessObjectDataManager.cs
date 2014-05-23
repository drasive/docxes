using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    // TODO: Set to "private"
    abstract class BusinessObjectDataManagerBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        protected LocalDatabaseContainer GetDatabaseContainer() {
            return new LocalDatabaseContainer();
        }


        public abstract void Create(BusinessObject objectToSave);

        public abstract void Update(BusinessObject objectToUpdate);

        public abstract void Delete(BusinessObject objectToDelete);

    }

    abstract class BusinessObjectDataManager<BusinessObject> : BusinessObjectDataManagerBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        public abstract List<BusinessObject> Get();

    }

    abstract class BusinessObjectDataManager<BusinessObject, BusinessObjectParent> : BusinessObjectDataManagerBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject
        where BusinessObjectParent : Docxes.IBusinessObject {

        public abstract List<BusinessObject> Get(BusinessObjectParent objectsParent);

    }

}
