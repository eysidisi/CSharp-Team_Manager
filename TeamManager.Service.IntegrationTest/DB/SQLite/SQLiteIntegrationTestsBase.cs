using System;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.UnitTest.HelperMethods.SQLiteDB;

namespace TeamManager.Service.IntegrationTest.DB.SQLite
{
    public abstract class SQLiteIntegrationTestsBase : IDisposable
    {
        protected SQLiteHelperMethods helperMethods = new SQLiteHelperMethods();
        protected SQLiteDatabaseManager databaseManager;
        protected string connString;
        protected string dbPath;
        public SQLiteIntegrationTestsBase()
        {
            dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            connString = $"Data Source={dbPath}";
            databaseManager = new SQLiteDatabaseManager(connString);
        }
        public void Dispose()
        {
            helperMethods.DeleteDBIfExists(dbPath);
        }
    }
}