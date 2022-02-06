using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard;
using Xunit;

namespace TeamManager.Service.Test.SQliteDB
{
    public class SQLiteDataAccessTests
    {
        [Fact]
        public void CheckIfUserExists_ReturnsTrue()
        {
            // Arrange

            string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\SQliteDB\TestDBs\TestDB.db; Version = 3";

            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User validUser = new User("validUserName", "validPassword");

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(validUser);

            // Assert
            Assert.True(isUserValid);

        }

        [Fact]
        public void CheckIfUserExists_InvalidUserNameInvalidPasswords_ReturnsFalse()
        {
            // Arrange

            string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\SQliteDB\TestDBs\TestDB.db; Version = 3";

            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User validUser = new User("invalidUserName", "invalidPassword");

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(validUser);

            // Assert
            Assert.False(isUserValid);

        }

        [Fact]
        public void CheckIfUserExists_ValidUserNameInvalidPasswords_ReturnsFalse()
        {
            // Arrange
            string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\SQliteDB\TestDBs\TestDB.db; Version = 3";

            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            User validUser = new User("validUserName", "invalidPassword");

            // Act
            var isUserValid = dataAccess.CheckIfUserExists(validUser);

            // Assert
            Assert.False(isUserValid);

        }

        [Fact]
        public void SavePurpose_SavesPurposeSuccessfully()
        {
            // Arrange
            string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\SQliteDB\TestDBs\TestDB.db; Version = 3";

            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string purposeText = "A purpose text";
            string userName = "username";

            Purpose purpose = new Purpose(userName, purposeText);

            // Act
            dataAccess.SavePurpose(purpose);

            // Assert
        }
    }
}
