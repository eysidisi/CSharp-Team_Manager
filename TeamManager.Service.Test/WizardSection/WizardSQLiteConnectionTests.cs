using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Models;
using TeamManager.Service.Test.Database.SQLiteDB;
using TeamManager.Service.WizardSection.Database;
using Xunit;

namespace TeamManager.Service.Test.WizardSection
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

            // Insert manager
            Manager expectedManager = new Manager(HelperMethods.validManagerUserName, HelperMethods.validManagerPassword)
            {
                ID = 1
            };
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(expectedManager);
            }

            // Act
            Manager actualManager = dataAccess.GetManager(HelperMethods.validManagerUserName);

            // Assert
            Assert.Equal(expectedManager.UserName, actualManager.UserName);
            Assert.Equal(expectedManager.Password, actualManager.Password);
            Assert.Equal(expectedManager.ID, actualManager.ID);

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
