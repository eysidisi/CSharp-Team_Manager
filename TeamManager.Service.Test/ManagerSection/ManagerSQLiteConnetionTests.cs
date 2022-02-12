using System;
using System.Collections.Generic;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using TeamManager.Service.Test.Database.SQLiteDB;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class ManagerSQLiteConnetionTests
    {
        [Fact]
        public void SaveUser_SavesUser()
        {
            //Arrange
            HelperMethods helperMethods = new HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

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
