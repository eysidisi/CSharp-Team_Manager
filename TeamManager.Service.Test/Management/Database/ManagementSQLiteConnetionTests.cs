﻿using Dapper.Contrib.Extensions;
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
            dbFilePath = sqliteHelperMethods.CreateEmptyTestDB_ReturnFilePath();
            connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            managementSQLiteConnetion = new ManagementSQLiteConnetion(connectionString);
        }

        [Fact]
        public void SaveUser_EmptyDB_SavesUser()
        {
            //Arrange
            var user = new User()
            {
                ID = 1,
                Name = "userName",
                Surname = "userSurname"
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
            // Arrange
            var team1 = new Team()
            {
                ID = 1,
                Name = "team1",
                CreationDate = "1234"
            };

            var team2 = new Team()
            {
                ID = 2,
                Name = "team2",
                CreationDate = "1234"
            };

            List<Team> expectedTeams = new List<Team>() { team1, team2 };
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(expectedTeams);
            }

            // Act
            var actualTeams = managementSQLiteConnetion.GetAllTeams();

            // Assert
            Assert.Equal(actualTeams, expectedTeams);
        }

        [Fact]
        public void GetAllTeams_NoTeamExistsInTheDB_ReturnsEmptyList()
        {
            // Act
            var savedTeams = managementSQLiteConnetion.GetAllTeams();

            // Assert
            Assert.Empty(savedTeams);
        }

        [Fact]
        public void DeleteTeam_TeamExistsInTheDB_DeletesTeam()
        {
            //Arrange
            var team = new Team()
            {
                ID = 1,
                Name = "team1",
                CreationDate = "1234"
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Act
            var deletionResult = managementSQLiteConnetion.DeleteTeam(team);

            // Assert
            Assert.True(deletionResult);
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInTheDB_CantDeleteTeam()
        {
            //Arrange

            var team = new Team()
            {
                ID = 0,
                Name = "team",
                CreationDate = "1234"
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Act
            // Object in the DB has ID 1
            team.ID = 0;
            var deletionResult = managementSQLiteConnetion.DeleteTeam(team);

            // Assert
            Assert.False(deletionResult);
        }

        [Fact]
        public void GetAllUserIDToTeamID_NoUserIDToTeamIDExistsInTheDB_ReturnsEmptyList()
        {
            // Act
            var savedTeams = managementSQLiteConnetion.GetAllUserIDToTeamID();

            // Assert
            Assert.Empty(savedTeams);
        }

        [Fact]
        public void GetAllUserIDToTeamID_UserIDToTeamIDExistsInTheDB_GetsAllUserIDToTeamID()
        {
            //Arrange
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
            {
                TeamID = 1,
                UserID = 1
            };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            var savedUserIDToTeamIDs = managementSQLiteConnetion.GetAllUserIDToTeamID();

            // Assert
            Assert.Contains(userIDToTeamID, savedUserIDToTeamIDs);
        }

        [Fact]
        public void SaveUserIDToTeamID_EmptyDB_SavesUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            // Act
            managementSQLiteConnetion.SaveUserIDToTeamID(userIDToTeamID);

            // Assert
            List<UserIDToTeamID> actualUserIDToTeamIDs;

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                actualUserIDToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.Contains(userIDToTeamID, actualUserIDToTeamIDs);
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDExistsInDB_DeletesUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            bool deletionResult = managementSQLiteConnetion.DeleteUserIDToTeamID(userIDToTeamID);

            // Assert
            Assert.True(deletionResult);
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDDoesntExistInDB_CantDeleteUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            userIDToTeamID.ID = 3;
            bool deletionResult = managementSQLiteConnetion.DeleteUserIDToTeamID(userIDToTeamID);

            // Assert
            Assert.False(deletionResult);
        }

        public void Dispose()
        {
            sqliteHelperMethods.DeleteDBIfExists(dbFilePath);
        }
    }
}