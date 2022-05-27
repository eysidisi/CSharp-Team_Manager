using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class UserDetailsPageServiceTestsSQLite : UserDetailsPageServiceTests
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
