using System.Collections.Generic;
using System.Linq;
using TeamManager.Service.Models;
using TeamManager.Service.Test.Database.SQLiteDB;
using TeamManager.Service.Wizard.Database;
using Xunit;

namespace TeamManager.Service.Test.Wizard
{
    public class WizardSQLiteConnectionTests
    {
        [Fact]
        public void GetManager_GetsManager()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

            // Act
            Manager manager = dataAccess.GetManager(HelperMethods.validManagerUserName);

            // Assert
            Assert.Equal(manager.UserName, HelperMethods.validManagerUserName);
            Assert.Equal(manager.Password, HelperMethods.validManagerPassword);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetManager_CantGetManager()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

            // Act
            Manager manager = dataAccess.GetManager("invalidName");

            // Assert
            Assert.Null(manager);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            WizardSQLiteConnection dataAccess = new WizardSQLiteConnection(connectionString);

            string purposeText = "A purpose text";
            string managerName = "Managername";

            Purpose purpose = new Purpose(managerName, purposeText);
            dataAccess.SavePurpose(purpose);

            // Act
            List<Purpose> purposes = dataAccess.GetPurposes(managerName);

            // Assert
            Assert.Contains(purposeText, purposes.Select(p => p.PurposeText).ToList());

            helperMethods.DeleteDB(dbFilePath);
        }
    }
}
