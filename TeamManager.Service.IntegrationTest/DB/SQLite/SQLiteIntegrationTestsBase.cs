using System;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;

namespace TeamManager.Service.IntegrationTest.DB.SQLite
{
    public abstract class SQLiteIntegrationTestsBase : IDisposable
    {
        protected HelperMethods helperMethods = new HelperMethods();
        protected ManagerSQLiteConnetion connection;
        protected string connString;
        protected string dbPath;
        public SQLiteIntegrationTestsBase()
        {
            dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            connString = $"Data Source={dbPath}";
            connection = new ManagerSQLiteConnetion(connString);
        }
        public void Dispose()
        {
            helperMethods.DeleteDB(dbPath);
        }
    }
}