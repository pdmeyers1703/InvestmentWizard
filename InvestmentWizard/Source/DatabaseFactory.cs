namespace InvestmentWizard
{
    using System;
    using System.IO;

    /// <summary>
    /// Creates User Databases
    /// </summary>
    public static class DatabaseFactory
    {
        private const string BlankDatabasePath = "..\\..\\DataBases\\BlankDataStore.accdb";
        private const string DatabaseName = "DataStore.accdb";
        private const string DataFolder = "//InvestmentWizard";
        private static readonly string DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + DataFolder;
        
        /// <summary>
        /// Creates and set path for database
        /// Determines if database exists and if uses a blank database
        /// </summary>
        /// <param name="database">newly created user database</param>
        /// <returns>user database assign with correct path</returns>
        public static IDatabase Create(IDatabase database)
        {
            string databaseFile = Path.Combine(DatabasePath, DatabaseName);
            
            if (File.Exists(databaseFile))
            {
                database.DatabasePath = databaseFile;
                return database;
            }
            else
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(DatabasePath);
                File.Copy(Path.GetFullPath(BlankDatabasePath), databaseFile);
                return database;
            }
        }
    }
}
