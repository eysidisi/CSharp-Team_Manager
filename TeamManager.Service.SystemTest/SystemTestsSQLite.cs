using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseControllers;

namespace TeamManager.Service.SystemTest
{
    public class SystemTestsSQLite : SystemTests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController(string connectionString)
        {
            return new ManagerSQLiteDatabaseController(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new SQLiteDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController(string connectionString)
        {
            return new WizardSQLiteDatabaseController(connectionString);
        }
    }
}
