using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseConnection;
using TeamManager.Service.Wizard.DatabaseController;
using TeamManager.Service.Wizard.Models;
using Xunit;

namespace TeamManager.Service.UnitTest.Wizard.Database
{
    public class WizardSQLiteDatabaseControllerTests
    {
        WizardDatabaseController databaseController;
        string connectionString;

        public WizardSQLiteDatabaseControllerTests()
        {
            SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();
            connectionString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            WizardSQLiteDatabaseConnection connection = new WizardSQLiteDatabaseConnection(connectionString);
            databaseController = new WizardDatabaseController(connection);
        }

        [Fact]
        public void GetManagers_ManagersExistInDB_GetsManagers()
        {
            //Arrange



            // Insert manager
            Manager expectedManager = new Manager(DatabaseTestHelper.ValidManagerUserName, SQLiteDatabaseTestHelper.ValidManagerPassword);

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(expectedManager);
            }

            // Act
            var allManagers = databaseController.GetManagers();

            // Assert
            Assert.Contains(allManagers, m => m.UserName == expectedManager.UserName && m.Password == expectedManager.Password);
        }

        [Fact]
        public void GetManager_NoManagerExistsInDB_ReturnsEmptyList()
        {
            //Arrange

            // Act
            var allManagers = databaseController.GetManagers();

            // Assert
            Assert.Empty(allManagers);
        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            //Arrange
            string purposeText = "A purpose text";
            string managerName = "Managername";

            Purpose purpose = new Purpose(managerName, purposeText);

            // Act
            databaseController.SavePurpose(purpose);

            // Assert
            List<Purpose> purposes;

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                purposes = cnn.GetAll<Purpose>().ToList();
            }

            Assert.Contains(purposeText, purposes.Select(p => p.PurposeText).ToList());
        }
    }
}
