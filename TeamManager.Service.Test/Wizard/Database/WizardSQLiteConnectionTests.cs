using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.UnitTest.HelperMethods.SQLiteDB;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service.Wizard.Models;
using Xunit;

namespace TeamManager.Service.UnitTest.Wizard.Database
{
    public class WizardSQLiteConnectionTests
    {
        [Fact]
        public void GetManagers_ManagersExistInDB_GetsManagers()
        {
            //Arrange
            SQLiteHelperMethods helperMethods = new SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardDatabaseManagerSQLite dataAccess = new WizardDatabaseManagerSQLite(connectionString);

            // Insert manager
            Manager expectedManager = new Manager(SQLiteHelperMethods.ValidManagerUserName, SQLiteHelperMethods.ValidManagerPassword);

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(expectedManager);
            }

            // Act
            var allManagers = dataAccess.GetManagers();

            // Assert
            Assert.Contains(allManagers, m => m.UserName == expectedManager.UserName && m.Password == expectedManager.Password);

            helperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void GetManager_NoManagerExistsInDB_ReturnsEmptyList()
        {
            //Arrange
            SQLiteHelperMethods helperMethods = new SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardDatabaseManagerSQLite dataAccess = new WizardDatabaseManagerSQLite(connectionString);

            // Act
            var allManagers = dataAccess.GetManagers();

            // Assert
            Assert.Empty(allManagers);

            helperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            //Arrange
            SQLiteHelperMethods helperMethods = new SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardDatabaseManagerSQLite dataAccess = new WizardDatabaseManagerSQLite(connectionString);

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

            helperMethods.DeleteDBIfExists(dbFilePath);
        }
    }
}
