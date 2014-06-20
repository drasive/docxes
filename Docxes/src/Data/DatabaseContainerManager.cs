
namespace VrankenBischof.Docxes.Data {

    // TODO: Document DatabaseContainerManager

    public sealed class DatabaseContainerManager {

        private static LocalDatabaseContainer localDatabaseContainer;

        public static LocalDatabaseContainer GetLocalDatabaseContainer() {
            if (localDatabaseContainer == null) {
                localDatabaseContainer = new LocalDatabaseContainer();
            }

            return localDatabaseContainer;
        }

    }

}
