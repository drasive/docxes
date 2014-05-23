using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    // TODO: Change this stuff to an interface (no common functionality)?

    // TODO: Mark as "private"
    abstract class BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        public abstract void Create(BusinessObject objectToSave);

        public abstract void Update(BusinessObject objectToUpdate);

        public abstract void Delete(BusinessObject objectToDelete);

    }

    abstract class BusinessObjectProcessor<BusinessObject> : BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<BusinessObject> dataManager;

        public abstract List<BusinessObject> Get();

    }

    abstract class BusinessObjectProcessor<BusinessObject, BusinessObjectParent> : BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject
        where BusinessObjectParent : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<BusinessObject, BusinessObjectParent> dataManager;

        //public abstract bool CanCreate();

        public abstract List<BusinessObject> Get(BusinessObjectParent objectsParent);

    }

}
