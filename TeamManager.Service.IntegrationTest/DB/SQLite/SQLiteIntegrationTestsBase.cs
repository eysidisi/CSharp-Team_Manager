using System;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;

namespace TeamManager.Service.IntegrationTest.DB.SQLite
{
    public abstract class SQLiteIntegrationTestsBase : IDisposable
    {
        protected SQLiteHelperMethods helperMethods = new SQLiteHelperMethods();
        protected ManagementSQLiteConnetion connection;
        protected string connString;
        protected string dbPath;
        public SQLiteIntegrationTestsBase()
        {
            dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            connString = $"Data Source={dbPath}";
            connection = new ManagementSQLiteConnetion(connString);
        }
        public void Dispose()
        {
            helperMethods.DeleteDBIfExists(dbPath);
        }
    }
}