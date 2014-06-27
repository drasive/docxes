namespace VrankenBischof.Docxes.Data {

    /// <summary>
    /// Manages the installation and disposition of all the database containers used throughout the application.
    /// </summary>
    internal sealed class DatabaseContainerManager {

        /// <summary>
        /// Returns a database container for the local application database.
        /// </summary>
        /// <returns>A database container for the local application database.</returns>
        internal static LocalDatabaseContainer GetLocalDatabaseContainer() {
            return new LocalDatabaseContainer();
        }

    }

}
