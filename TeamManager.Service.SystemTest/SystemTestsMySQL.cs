using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseControllers;

namespace TeamManager.Service.SystemTest
{
    public class SystemTestsMySQL : SystemTests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController(string connectionString)
        {
            return new ManagerMySQLDatabaseController(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController(string connectionString)
        {
            return new WizardMySQLDatabaseController(connectionString);
        }
    }
}
