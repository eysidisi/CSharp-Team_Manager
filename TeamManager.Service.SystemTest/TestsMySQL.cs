using MySql.Data.MySqlClient;
using System.Data;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseControllers;

namespace TeamManager.Service.SystemTests
{
    public class TestsMySQL : Tests
    {
        protected override ManagerDatabaseController CreateManagerDatabaseController()
        {
            return new ManagerMySQLDatabaseController(connectionString);
        }

        protected override DatabaseTestHelper CreateDatabaseTestHelper()
        {
            return new MySqlDatabaseTestHelper();
        }

        protected override WizardDatabaseController CreateWizardDatabaseController()
        {
            return new WizardMySQLDatabaseController(connectionString);
        }

        protected override IDbConnection CreateConnection()
        {
           return new MySqlConnection(connectionString);
        }
    }
}
