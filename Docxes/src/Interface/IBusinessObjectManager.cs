
namespace VrankenBischof.Docxes.Interface {

    public interface IBusinessObjectManager {

        bool IsEditing { get; }

        BusinessObjectManagerAction Action { get; }

    }

}
