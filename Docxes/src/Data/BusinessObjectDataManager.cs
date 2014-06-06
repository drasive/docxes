using System.Collections.Generic;

namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Provides basic functionality to manage a <see cref="IBusinessObject"/> in nonvolatile memory.
    /// </summary>
    /// <typeparam name="Entity">The type of entity to manage.</typeparam>
    public abstract class BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject {

        protected LocalDatabaseContainer GetDatabaseContainer() {
            return new LocalDatabaseContainer();
        }


        /// <summary>
        /// Saves a new entity.
        /// </summary>
        /// <param name="entityToSave">The entity to save.</param>
        public abstract void Create(Entity entityToSave);

        /// <summary>
        /// Updates the properties of an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity with the updated properties.</param>
        public abstract void Update(Entity entityToUpdate);

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public abstract void Delete(Entity entityToDelete);

    }

    /// <summary>
    /// Provides functionality to manage a <see cref="IBusinessObject"/> without a parent entity in nonvolatile memory.
    /// </summary>
    /// <typeparam name="Entity">The type of entity to manage.</typeparam>
    public abstract class BusinessObjectDataManager<Entity> : BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject {

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public abstract List<Entity> Get();

    }

    /// <summary>
    /// Provides functionality to manage a <see cref="IBusinessObject"/> with a parent entity in nonvolatile memory.
    /// </summary>
    /// <typeparam name="Entity">The type of entity to manage.</typeparam>
    /// <typeparam name="EntityParent">The type of the parent of the entity to manage.</typeparam>
    public abstract class BusinessObjectDataManager<Entity, EntityParent> : BusinessObjectDataManagerBase<Entity>
        where Entity : Docxes.IBusinessObject
        where EntityParent : Docxes.IBusinessObject {

        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>A list of all existing entities.</returns>
        public abstract List<Entity> Get();

        /// <summary>
        /// Gets all existing entities with the specified parent.
        /// </summary>
        /// <param name="entitiesParent">The parent that the returned entities must have.</param>
        /// <returns>A list of all existing entities with the specified parent.</returns>
        public abstract List<Entity> Get(EntityParent entitiesParent);

    }

}
