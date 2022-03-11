using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.Test.Management
{
    public class ManagerSQLiteConnetionTests
    {
        [Fact]
        public void SaveUser_EmptyDB_SavesUser()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
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
        public void DeleteUser_UserExistsInTheDB_DeletesUser()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            int userID = 1;

            var user = new User()
            {
                ID = userID,
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(user);
            }

            // Assert
            Assert.True(dataAccess.DeleteUser(user));

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteUser_UserDoesntExistInTheDB_CantDeleteUser()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);


            var userToAdd = new User() { };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userToAdd);
            }
            var userToDelete = new User() { ID = 2 };

            // Assert
            Assert.False(dataAccess.DeleteUser(userToDelete));

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void SaveTeam_EmptyDB_SavesTeam()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
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

            List<Team> allTeams;
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                allTeams = cnn.GetAll<Team>().ToList();
            }

            // Assert
            Assert.Contains(allTeams, t => t.Name == team.Name && t.CreationDate == team.CreationDate);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetAllTeams_TeamsExistInDB_GetsAllTeams()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
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

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(team1);
                cnn.Insert(team2);
            }

            // Act
            var savedTeams = dataAccess.GetAllTeams();

            // Assert
            Assert.Equal(2, savedTeams.Count);
            Assert.Contains(savedTeams, t => t.Name == teamName1 && t.CreationDate == creationDate1);
            Assert.Contains(savedTeams, t => t.Name == teamName2 && t.CreationDate == creationDate2);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetAllTeams_NoTeamExistsInTheDB_ReturnsEmptyList()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            // Act
            var savedTeams = dataAccess.GetAllTeams();

            // Assert
            Assert.Empty(savedTeams);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_TeamExistsInTheDB_DeletesTeam()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            string teamName = "team1";
            string creationDate = "1234";
            int teamID = 1;
            var team = new Team()
            {
                ID = teamID,
                Name = teamName,
                CreationDate = creationDate
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Assert
            Assert.True(dataAccess.DeleteTeam(team));

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInTheDB_CantDeleteTeam()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            string teamName1 = "team1";
            string creationDate1 = "1234";

            var team = new Team()
            {
                Name = teamName1,
                CreationDate = creationDate1
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Object in the DB has ID 1
            team.ID = 0;

            // Assert
            Assert.False(dataAccess.DeleteTeam(team));

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetAllUserIDToTeamID_NoUserIDToTeamIDExistsInTheDB_ReturnsEmptyList()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            // Act
            var savedTeams = dataAccess.GetAllUserIDToTeamID();

            // Assert
            Assert.Empty(savedTeams);

            helperMethods.DeleteDB(dbFilePath);
        }

        [Fact]
        public void GetAllUserIDToTeamID_UserIDToTeamIDExistsInTheDB_GetsAllUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            int teamID = 1;
            int userID = 1;

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
            {
                TeamID = teamID,
                UserID = userID
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            var savedUserIDToTeamIDs = dataAccess.GetAllUserIDToTeamID();

            // Assert
            Assert.Contains(savedUserIDToTeamIDs, t => t.UserID == userID && t.TeamID == teamID);

            helperMethods.DeleteDB(dbFilePath);
        }


        [Fact]
        public void SaveUserIDToTeamID_EmptyDB_SavesUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            // Act
            dataAccess.SaveUserIDToTeamID(userIDToTeamID);

            // Assert
            List<UserIDToTeamID> allUserIDToTeamIDs;

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                allUserIDToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.Contains(allUserIDToTeamIDs, a => a.UserID == userID && a.TeamID == teamID);
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDExistsInDB_DeletesUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            Assert.True(dataAccess.DeleteUserIDToTeamID(userIDToTeamID));
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDDoesntExistInDB_CantDeleteUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.HelperMethods helperMethods = new HelperMethods.SQLiteDB.HelperMethods();
            string dbFilePath = helperMethods.CreateTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagerSQLiteConnetion dataAccess = new ManagerSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            userIDToTeamID.ID = 3;
            Assert.False(dataAccess.DeleteUserIDToTeamID(userIDToTeamID));
        }

    }
}
