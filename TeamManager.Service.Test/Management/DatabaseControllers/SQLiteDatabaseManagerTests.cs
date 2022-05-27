using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseManagers
{
    public class SQLiteDatabaseManagerTests : DatabaseManagerTestsBaseClass
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new SQLiteDatabaseTestHelper();
        }

        protected override ManagerDatabaseController CreateDatabaseManager(string connectionString)
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }
    }
}
