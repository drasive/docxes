using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VrankenBischof.Docxes.Data {

    // TODO: Set to "private"
    abstract class BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject {

        protected LocalDatabaseContainer GetDatabaseContainer() {
            return new LocalDatabaseContainer();
        }


        public abstract void Create(Entity entityToSave);

        public abstract void Update(Entity entityToUpdate);

        public abstract void Delete(Entity entityToDelete);

    }

    abstract class BusinessObjectDataManager<Entity> : BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject {

        public abstract List<Entity> Get();

    }

    abstract class BusinessObjectDataManager<Entity, EntityParent> : BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject
        where EntityParent : Docxes.IBusinessObject {

        public abstract List<Entity> Get(EntityParent entitiesParent);

    }

}
