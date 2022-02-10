using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Database;
using Xunit;
using TeamManager.Service.Models;

namespace TeamManager.Service.Test.Database.SQLiteDB
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

        [Fact]
        public void SaveUser_SavesUser()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string userName = "userName";
            string userSurname = "userSurname";

            var user = new User()
            {
                Name = userName,
                Surname = userSurname
            };

            // Act
            dataAccess.SaveUser(user);

            List<User> users = dataAccess.GetUsers();

            //// Assert
            Assert.Contains(users, u => u.Name == userName && u.Surname == userSurname);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteUser_DeletessUser()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string userName = "userName";
            string userSurname = "userSurname";

            var user = new User()
            {
                Name = userName,
                Surname = userSurname
            };

            // Act
            dataAccess.SaveUser(user);

            List<User> users = dataAccess.GetUsers();

            User savedUser = users.Find(u => u.Name == userName && u.Surname == userSurname);

            // Assert
            Assert.True(dataAccess.DeleteUser(savedUser));

            helperMethods.DeleteDB(dbFilePath);
        }

    }
}
