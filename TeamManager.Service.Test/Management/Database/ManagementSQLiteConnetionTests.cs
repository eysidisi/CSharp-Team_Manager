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
    public class ManagementSQLiteConnetionTests : IDisposable
    {
        SQLiteHelperMethods sqliteHelperMethods;
        string dbFilePath;
        string connectionString;
        ManagementSQLiteConnetion managementSQLiteConnetion;

        public ManagementSQLiteConnetionTests()
        {
            sqliteHelperMethods = new SQLiteHelperMethods();
            string dbFilePath = sqliteHelperMethods.CreateEmptyTestDB_ReturnFilePath();
            connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);
        }

        [Fact]
        public void SaveUser_EmptyDB_SavesUser()
        {
            //Arrange
            string userName = "userName";
            string userSurname = "userSurname";

            var user = new User()
            {
                ID = 1,
                Name = userName,
                Surname = userSurname
            };

            // Act
            managementSQLiteConnetion.SaveUser(user);

            List<User> actualUsers;
            using (var con = new SQLiteConnection(connectionString))
            {
                actualUsers = con.GetAll<User>().ToList();
            }

            // Assert
            Assert.Contains(user, actualUsers);
        }

        [Fact]
        public void DeleteUser_UserExistsInTheDB_DeletesUser()
        {
            //Arrange
            var user = new User()
            {
                ID = 1,
                Name = "user"
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(user);
            }

            // Assert
            Assert.True(managementSQLiteConnetion.DeleteUser(user));
        }

        [Fact]
        public void DeleteUser_UserDoesntExistInTheDB_CantDeleteUser()
        {
            //Arrange
            var userToAdd = new User() { ID = 1 };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userToAdd);
            }
            var userToDelete = new User() { ID = 2 };

            // Assert
            Assert.False(managementSQLiteConnetion.DeleteUser(userToDelete));
        }

        [Fact]
        public void SaveTeam_EmptyDB_SavesTeam()
        {
            //Arrange
            var team = new Team()
            {
                ID = 1,
                Name = "team",
                CreationDate = "1234"
            };

            // Act
            managementSQLiteConnetion.SaveTeam(team);

            List<Team> allTeams;
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                allTeams = cnn.GetAll<Team>().ToList();
            }

            // Assert
            Assert.Contains(team, allTeams);
        }

        [Fact]
        public void GetAllTeams_TeamsExistInDB_GetsAllTeams()
        {
            //Arrange
            SQLiteHelperMethods sqliteHelperMethods = new SQLiteHelperMethods();
            string dbFilePath = sqliteHelperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

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
            var savedTeams = managementSQLiteConnetion.GetAllTeams();

            // Assert
            Assert.Equal(2, savedTeams.Count);
            Assert.Contains(savedTeams, t => t.Name == teamName1 && t.CreationDate == creationDate1);
            Assert.Contains(savedTeams, t => t.Name == teamName2 && t.CreationDate == creationDate2);

            sqliteHelperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void GetAllTeams_NoTeamExistsInTheDB_ReturnsEmptyList()
        {
            //Arrange
            SQLiteHelperMethods sqliteHelperMethods = new SQLiteHelperMethods();
            string dbFilePath = sqliteHelperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

            // Act
            var savedTeams = managementSQLiteConnetion.GetAllTeams();

            // Assert
            Assert.Empty(savedTeams);

            sqliteHelperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_TeamExistsInTheDB_DeletesTeam()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

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
            Assert.True(managementSQLiteConnetion.DeleteTeam(team));

            helperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInTheDB_CantDeleteTeam()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

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
            Assert.False(managementSQLiteConnetion.DeleteTeam(team));

            helperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void GetAllUserIDToTeamID_NoUserIDToTeamIDExistsInTheDB_ReturnsEmptyList()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

            // Act
            var savedTeams = managementSQLiteConnetion.GetAllUserIDToTeamID();

            // Assert
            Assert.Empty(savedTeams);

            helperMethods.DeleteDBIfExists(dbFilePath);
        }

        [Fact]
        public void GetAllUserIDToTeamID_UserIDToTeamIDExistsInTheDB_GetsAllUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

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
            var savedUserIDToTeamIDs = managementSQLiteConnetion.GetAllUserIDToTeamID();

            // Assert
            Assert.Contains(savedUserIDToTeamIDs, t => t.UserID == userID && t.TeamID == teamID);

            helperMethods.DeleteDBIfExists(dbFilePath);
        }


        [Fact]
        public void SaveUserIDToTeamID_EmptyDB_SavesUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            // Act
            managementSQLiteConnetion.SaveUserIDToTeamID(userIDToTeamID);

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
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            Assert.True(managementSQLiteConnetion.DeleteUserIDToTeamID(userIDToTeamID));
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDDoesntExistInDB_CantDeleteUserIDToTeamID()
        {
            //Arrange
            HelperMethods.SQLiteDB.SQLiteHelperMethods helperMethods = new HelperMethods.SQLiteDB.SQLiteHelperMethods();
            string dbFilePath = helperMethods.CreateEmptyTestDB_ReturnFilePath();

            string connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            ManagementSQLiteConnetion managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);

            int userID = 1;
            int teamID = 1;

            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userID, TeamID = teamID };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            userIDToTeamID.ID = 3;
            Assert.False(managementSQLiteConnetion.DeleteUserIDToTeamID(userIDToTeamID));
        }

        public void Dispose()
        {
            sqliteHelperMethods.DeleteDBIfExists(dbFilePath);
        }
    }
}
