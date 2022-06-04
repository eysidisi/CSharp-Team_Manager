using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseControllers
{
    public class MySQLdatabaseControllerTests : DatabaseControllerTestsBaseClass
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseHelperMethods()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            var connection = new ManagerMySQLDatabaseConnection(connectionString);
            return new ManagerDatabaseController(connection);
        }
    }
}
