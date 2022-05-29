using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseControllers;

namespace TeamManager.Service.SystemTests
{
    public class TestsSQLite : Tests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController()
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new SQLiteDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController()
        {
            return new WizardSQLiteDatabaseController(connectionString);
        }

        protected override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
