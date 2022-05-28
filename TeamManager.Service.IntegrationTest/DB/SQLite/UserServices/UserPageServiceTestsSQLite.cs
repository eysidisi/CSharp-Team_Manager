using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using UserPageServiceTests = TeamManager.Service.IntegrationTest.DB.UserServices.UserPageServiceTests;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class UserPageServiceTestsSQLite : UserPageServiceTests
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new SQLiteDatabaseTestHelper();
        }
    }
}
