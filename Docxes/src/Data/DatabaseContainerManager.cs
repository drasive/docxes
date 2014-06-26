namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Manages the installation and disposition of all the database containers used throughout the application.
    /// </summary>
    internal sealed class DatabaseContainerManager {

        private static LocalDatabaseContainer localDatabaseContainer;

        /// <summary>
        /// Returns a database container for the use with the local application database.
        /// </summary>
        /// <returns>A database container for the use with the local application database.</returns>
        internal static LocalDatabaseContainer GetLocalDatabaseContainer(bool useCache = true) {
            // TODO: HOLY FUCKING SHIT, when does EF require a new DB connection and when does requre the existing one to work properly?
            if (localDatabaseContainer == null || !useCache || true) {
                localDatabaseContainer = new LocalDatabaseContainer();
            }

            return localDatabaseContainer;
        }

    }

}
