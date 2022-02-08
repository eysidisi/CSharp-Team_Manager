using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;
using Xunit;

namespace TeamManager.Service.Test.SQliteDB
{
    public class SQLiteDataAccessTests
    {

        [Fact]
        public void CheckIfUserExists_ReturnsTrue()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User validUser = new User(HelperMethods.validUserName, HelperMethods.validPassword);

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(validUser);


            // Assert
            Assert.True(isUserValid);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void CheckIfUserExists_InvalidUserNameInvalidPasswords_ReturnsFalse()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User invalidUser = new User("invalidUserName", "invalidPassword");

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(invalidUser);

            // Assert
            Assert.False(isUserValid);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void CheckIfUserExists_ValidUserNameInvalidPasswords_ReturnsFalse()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User invalidUser = new User("validUserName", "invalidPassword");

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(invalidUser);

            // Assert
            Assert.False(isUserValid);

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
            string userName = "username";

            Purpose purpose = new Purpose(userName, purposeText);
            dataAccess.SavePurpose(purpose);

            // Act
            List<Purpose> purposes = dataAccess.GetPurposes(userName);

            // Assert
            Assert.Contains(purposeText, purposes.Select(p => p.PurposeText).ToList());

            helperMethods.DeleteDB(dbFilePath);
        }
    }
}
