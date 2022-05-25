using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.SQLite
{
    public abstract class SQLiteIntegrationTestsBase
    {
        protected SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();
        protected SQLiteDatabaseManager databaseManager;
        protected string connString;
        public SQLiteIntegrationTestsBase()
        {
            connString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            databaseManager = new SQLiteDatabaseManager(connString);
        }
    }
}