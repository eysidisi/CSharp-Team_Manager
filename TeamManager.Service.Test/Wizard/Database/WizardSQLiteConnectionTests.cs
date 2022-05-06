using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using TeamManager.Service.Wizard.Database;
using Xunit;

namespace TeamManager.Service.Test.Wizard
{
    public class WizardSQLiteConnectionTests
    {
        [Fact]
        public void GetManagers_ManagersExistInDB_GetsManagers()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

            // Insert manager
            Manager expectedManager = new Manager(HelperMethods.SQLiteDB.SQLiteHelperMethods.ValidManagerUserName, HelperMethods.SQLiteDB.SQLiteHelperMethods.ValidManagerPassword);

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
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

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
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

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
