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

            List<User> users = dataAccess.GetAllUsers();

            //// Assert
            Assert.Contains(users, u => u.Name == userName && u.Surname == userSurname);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteUser_DeletesUser()
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

            List<User> users = dataAccess.GetAllUsers();

            User savedUser = users.Find(u => u.Name == userName && u.Surname == userSurname);

            // Assert
            Assert.True(dataAccess.DeleteUser(savedUser));

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void SaveTeam_SavesTeam()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string teamName = "team1";
            string creationDate = "1234";

            var team = new Team()
            {
                Name = teamName,
                CreationDate = creationDate
            };

            // Act
            dataAccess.SaveTeam(team);

            Team savedTeam = dataAccess.GetTeamWithName(teamName);

            // Assert
            Assert.Equal(teamName, savedTeam.Name);
            Assert.Equal(creationDate, savedTeam.CreationDate);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetAllTeams_GetsAllTeams()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string teamName1 = "team1";
            string creationDate1 = "1234";

            string teamName2 = "team2";
            string creationDate2 = "1234";

            var team1 = new Team()
            {
                Name = teamName1,
                CreationDate = creationDate1
            };

            var team2 = new Team()
            {
                Name = teamName2,
                CreationDate = creationDate2
            };

            // Act
            dataAccess.SaveTeam(team1);
            dataAccess.SaveTeam(team2);

            var savedTeams = dataAccess.GetAllTeams();

            // Assert
            Assert.Equal(2, savedTeams.Count);
            Assert.Contains(savedTeams, t => t.Name == teamName1 && t.CreationDate == creationDate1);
            Assert.Contains(savedTeams, t => t.Name == teamName2 && t.CreationDate == creationDate2);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_DeletesTeam()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string teamName1 = "team1";
            string creationDate1 = "1234";

            var team1 = new Team()
            {
                Name = teamName1,
                CreationDate = creationDate1
            };

            dataAccess.SaveTeam(team1);

            // Act
            team1.ID = 1;
            var result = dataAccess.DeleteTeam(team1);

            // Assert
            Assert.True(result);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_CantDeleteTeam()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(connectionString);

            string teamName1 = "team1";
            string creationDate1 = "1234";

            var team1 = new Team()
            {
                Name = teamName1,
                CreationDate = creationDate1
            };

            dataAccess.SaveTeam(team1);

            // Act
            team1.ID = 0;
            var result = dataAccess.DeleteTeam(team1);

            // Assert
            Assert.False(result);

            helperMethods.DeleteDB(dbFilePath);
        }
    }
}
