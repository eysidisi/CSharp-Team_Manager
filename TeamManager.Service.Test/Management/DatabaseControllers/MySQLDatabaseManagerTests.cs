using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseControllers
{
    public class MySQLDatabaseManagerTests : DatabaseControllerTestsBaseClass
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override ManagerDatabaseController CreateDatabaseController(string connectionString)
        {
            return new ManagerMySQLDatabaseController(connectionString);
        }
    }
}
