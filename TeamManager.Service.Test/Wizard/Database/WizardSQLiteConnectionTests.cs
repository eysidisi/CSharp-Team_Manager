using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard.DatabaseControllers;
using TeamManager.Service.Wizard.Models;
using Xunit;

namespace TeamManager.Service.UnitTest.Wizard.Database
{
    public class WizardSQLiteDatabaseControllerTests
    {
        [Fact]
        public void GetManagers_ManagersExistInDB_GetsManagers()
        {
            //Arrange
            SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();

            string connectionString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            WizardSQLiteDatabaseController dataAccess = new WizardSQLiteDatabaseController(connectionString);

            // Insert manager
            Manager expectedManager = new Manager(DatabaseTestHelper.ValidManagerUserName, SQLiteDatabaseTestHelper.ValidManagerPassword);

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(expectedManager);
            }

            // Act
            var allManagers = dataAccess.GetManagers();

            // Assert
            Assert.Contains(allManagers, m => m.UserName == expectedManager.UserName && m.Password == expectedManager.Password);
        }

        [Fact]
        public void GetManager_NoManagerExistsInDB_ReturnsEmptyList()
        {
            //Arrange
            SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();

            string connectionString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            WizardSQLiteDatabaseController dataAccess = new WizardSQLiteDatabaseController(connectionString);

            // Act
            var allManagers = dataAccess.GetManagers();

            // Assert
            Assert.Empty(allManagers);
        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            //Arrange
            SQLiteDatabaseTestHelper helperMethods = new SQLiteDatabaseTestHelper();

            string connectionString = helperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            WizardSQLiteDatabaseController dataAccess = new WizardSQLiteDatabaseController(connectionString);

            string purposeText = "A purpose text";
            string managerName = "Managername";

            Purpose purpose = new Purpose(managerName, purposeText);

            // Act
            dataAccess.SavePurpose(purpose);

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
