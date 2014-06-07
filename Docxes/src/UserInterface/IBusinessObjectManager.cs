
namespace VrankenBischof.Docxes.UserInterface {

    public interface IBusinessObjectManager {

        bool IsEditing { get; }

        BusinessObjectManagerAction Action { get; }

    }

}
