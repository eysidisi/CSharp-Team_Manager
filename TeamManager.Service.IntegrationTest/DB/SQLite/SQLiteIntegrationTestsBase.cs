using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.SQLite
{
    public abstract class SQLiteIntegrationTestsBase
    {
        protected SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();
        protected ManagerSQLiteDatabaseController databaseManager;
        protected string connString;
        public SQLiteIntegrationTestsBase()
        {
            connString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            databaseManager = new ManagerSQLiteDatabaseController(connString);
        }
    }
}