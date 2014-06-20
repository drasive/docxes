using System.Collections.Generic;
using VrankenBischof.Docxes.Data;

namespace VrankenBischof.Docxes.BusinessLogic {

    /// <summary>
    /// Provides basic functionality to process a <see cref="IBusinessObject"/>.
    /// </summary>
    /// <typeparam name="BusinessObject">The type of business object to process.</typeparam>
    public abstract class BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        /// <summary>
        /// Saves a new business object to nonvolatile memory.
        /// </summary>
        /// <param name="objectToSave">The business object to save.</param>
        public abstract void Create(BusinessObject objectToSave);

        /// <summary>
        /// Updates the properties of an existing business objects.
        /// </summary>
        /// <param name="objectToUpdate">The business object with the updated properties.</param>
        public abstract void Update(BusinessObject objectToUpdate);

        /// <summary>
        /// Deletes an existing business object.
        /// </summary>
        /// <param name="objectToDelete">The business object to delete.</param>
        public abstract void Delete(BusinessObject objectToDelete);

    }

    /// <summary>
    /// Provides functionality to process a <see cref="IBusinessObject"/> without a parent business object.
    /// </summary>
    /// <typeparam name="BusinessObject">The type of business object to process.</typeparam>
    public abstract class BusinessObjectProcessor<BusinessObject> : BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<BusinessObject> dataManager;


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public abstract List<BusinessObject> Get();

    }

    /// <summary>
    /// Provides functionality to process a <see cref="IBusinessObject"/> with a parent business object.
    /// </summary>
    /// <typeparam name="BusinessObject">The type of business object to process.</typeparam>
    /// <typeparam name="BusinessObjectParent">The type of the parent of the business object to manage.</typeparam>
    public abstract class BusinessObjectProcessor<BusinessObject, BusinessObjectParent> : BusinessObjectProcessorBase<BusinessObject>
        where BusinessObject : Docxes.IBusinessObject
        where BusinessObjectParent : Docxes.IBusinessObject {

        protected BusinessObjectDataManager<BusinessObject, BusinessObjectParent> dataManager;


        /// <summary>
        /// Indicates whether a new object can be saved.
        /// </summary>
        /// <param name="parent">The parent of the object, on which the ability to save a new object is based on.</param>
        /// <returns>True if a new object can be saved; otherwise, false.</returns>
        public abstract bool CanCreate(IBusinessObject parent);


        /// <summary>
        /// Gets all existing business objects.
        /// </summary>
        /// <returns>A list of all existing business objects.</returns>
        public abstract List<BusinessObject> Get();

        /// <summary>
        /// Gets all existing business objects with the specified parent.
        /// </summary>
        /// <param name="objectsParent">The parent that the returned business objects must have.</param>
        /// <returns>A list of all existing business objects with the specified parent.</returns>
        public abstract List<BusinessObject> Get(BusinessObjectParent objectsParent);

    }

}
