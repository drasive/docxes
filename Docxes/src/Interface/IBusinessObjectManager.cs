
namespace VrankenBischof.Docxes.Interface {

    public interface IBusinessObjectManager {

        bool IsEditing { get; }

        BusinessObjectManagerAction Action { get; }

        // TODO: Some window methods, so explicit conversion to .ShowDialog() isn't needed in the IBusinessObjectManagers

    }

}
