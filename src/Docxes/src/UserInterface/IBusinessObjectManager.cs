namespace VrankenBischof.Docxes.UserInterface {

    /// <summary>
    /// Provides functionality to manage a business object.
    /// </summary>
    internal interface IBusinessObjectManager {

        bool IsEditing { get; }

        BusinessObjectManagerAction Action { get; }

    }

}
