using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Database;
using Xunit;
using TeamManager.Service.Models;

namespace TeamManager.Service.Test.SQliteDB
{
    public class SQLiteDataAccessTests
    {

        [Fact]
        public void CheckIfManagerExists_ReturnsTrue()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            var validManager = new Manager(HelperMethods.validManagerName, HelperMethods.validPassword);

            // Act
            var isManagerValid = dataAccess.CheckIfManagerExists(validManager);


            // Assert
            Assert.True(isManagerValid);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void CheckIfManagerExists_InvalidManagerNameInvalidPasswords_ReturnsFalse()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            var invalidManager = new Manager("invalidManagerName", "invalidPassword");

            // Act
            var isManagerValid = dataAccess.CheckIfManagerExists(invalidManager);

            // Assert
            Assert.False(isManagerValid);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void CheckIfManagerExists_ValidManagerNameInvalidPasswords_ReturnsFalse()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            var invalidManager = new Manager("validManagerName", "invalidPassword");

            // Act
            var isManagerValid = dataAccess.CheckIfManagerExists(invalidManager);

            // Assert
            Assert.False(isManagerValid);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string purposeText = "A purpose text";
            string ManagerName = "Managername";

            Purpose purpose = new Purpose(ManagerName, purposeText);
            dataAccess.SavePurpose(purpose);

            // Act
            List<Purpose> purposes = dataAccess.GetPurposes(ManagerName);

            // Assert
            Assert.Contains(purposeText, purposes.Select(p => p.PurposeText).ToList());

            helperMethods.DeleteDB(dbFilePath);
        }
    }
}
