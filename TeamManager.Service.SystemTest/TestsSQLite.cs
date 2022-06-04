using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseConnection;
using TeamManager.Service.Wizard.DatabaseController;

namespace TeamManager.Service.SystemTests
{
    public class TestsSQLite : Tests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController()
        {
            var connection = new ManagerSQLiteDatabaseConnection(connectionString);
            return new ManagerDatabaseController(connection);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new SQLiteDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController()
        {
            var connection = new WizardSQLiteDatabaseConnection(connectionString);
            return new WizardDatabaseController(connection);
        }

        protected override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
