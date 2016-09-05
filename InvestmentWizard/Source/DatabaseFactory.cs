namespace InvestmentWizard
{
    /// <summary>
    /// Creates User Databases
    /// </summary>
    public static class DatabaseFactory
    {
        /// <summary>
        /// Creates and set path for database
        /// </summary>
        /// <param name="database">newly created user database</param>
        /// <returns>user database assign with correct path</returns>
        public static IDatabase Create(IDatabase database)
        {
            database.DatabasePath = "..\\..\\DataBases\\DataStore.accdb";
            return database;
        }
    }
}
