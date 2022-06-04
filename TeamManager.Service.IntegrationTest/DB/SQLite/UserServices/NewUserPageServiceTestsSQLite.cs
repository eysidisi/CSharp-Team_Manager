using System.Data;
using System.Data.SQLite;
using TeamManager.Service.IntegrationTest.DB.UserServices;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class NewUserPageServiceTestsSQLite : NewUserPageServiceTests
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            var connection = new ManagerSQLiteDatabaseConnection(connectionString);
            return new ManagerDatabaseController(connection);
        }

        protected override DatabaseTestHelper CreateDatabaseHelperMethods()
        {
            return new SQLiteDatabaseTestHelper();
        }
    }
}