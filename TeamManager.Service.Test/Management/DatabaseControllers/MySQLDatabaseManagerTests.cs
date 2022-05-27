using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseManagers
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

        protected override ManagerDatabaseController CreateDatabaseManager(string connectionString)
        {
            return new ManagerMySQLDatabaseController(connectionString);
        }
    }
}
