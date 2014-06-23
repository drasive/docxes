namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Manages the installation and disposition of all the database containers used throughout the application.
    /// </summary>
    public sealed class DatabaseContainerManager {

        private static LocalDatabaseContainer localDatabaseContainer;

        /// <summary>
        /// Returns a database container for the use with the local application database.
        /// </summary>
        /// <returns>A database container for the use with the local application database.</returns>
        public static LocalDatabaseContainer GetLocalDatabaseContainer() {
            if (localDatabaseContainer == null) {
                localDatabaseContainer = new LocalDatabaseContainer();
            }

            return localDatabaseContainer;
        }

    }

}
