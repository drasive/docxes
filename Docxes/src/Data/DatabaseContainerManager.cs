using System.Collections.Generic;

namespace VrankenBischof.Docxes.Data {

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
