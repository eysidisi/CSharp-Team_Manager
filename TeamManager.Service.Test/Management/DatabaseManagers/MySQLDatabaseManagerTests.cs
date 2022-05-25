using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.UnitTest.HelperMethods.Database;

namespace TeamManager.Service.UnitTest.Management.DatabaseManagers
{
    public class MySQLDatabaseManagerTests : DatabaseManagerTestsBaseClass
    {
        protected override IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        protected override DatabaseTestHelperBase CreateDatabaseHelperMethods()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override DatabaseManager CreateDatabaseManager(string connectionString)
        {
            return new MySQLDatabaseManager(connectionString);
        }
    }
}
