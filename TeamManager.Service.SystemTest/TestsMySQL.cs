using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseConnection;
using TeamManager.Service.Wizard.DatabaseController;

namespace TeamManager.Service.SystemTests
{
    public class TestsMySQL : Tests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController()
        {
            var connection = new ManagerMySQLDatabaseConnection(connectionString);
            return new ManagerDatabaseController(connection);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController()
        {
            var connection = new WizardMySQLDatabaseConnection(connectionString);
            return new WizardDatabaseController(connection);
        }

        protected override IDbConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
