using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.DatabaseControllers
{
    public abstract class DatabaseControllerTestsBaseClass : IDisposable
    {
        readonly DatabaseTestHelperBase databaseTestHelperMethods;
        readonly string connectionString;
        readonly ManagerDatabaseController databaseManager;

        public DatabaseControllerTestsBaseClass()
        {
            databaseTestHelperMethods = CreateDatabaseHelperMethods();
            connectionString = databaseTestHelperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            databaseManager = CreateDatabaseManager(connectionString);
        }

        [Fact]
        public void GetAllUsers_EmptyDB_ReturnsEmptyList()
        {
            var users = databaseManager.GetAllUsers();

            Assert.Empty(users);
        }

        [Fact]
        public void GetAllUsers_SaveNewUser_GetAllUsers_UsersExistInTheDB_ReturnsUsers()
        {
            var user1 = new User()
            {
                ID = 1,
                Name = "user1"
            };

            var user2 = new User()
            {
                ID = 2,
                Name = "user2"
            };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(user1);
            }

            var actualUsers = databaseManager.GetAllUsers();

            Assert.Equal(new List<User>() { user1 }, actualUsers);

            databaseManager.SaveUser(user2);

            actualUsers = databaseManager.GetAllUsers();

            Assert.Equal(new List<User>() { user1, user2 }, actualUsers);

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

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(user);
            }

            // Act
            bool deletionResult = databaseManager.DeleteUser(user);

            // Assert
            Assert.True(deletionResult);
        }

        [Fact]
        public void GetAllUsers_DeleteUser_GetAllUSers_UserExistsInTheDB_ReturnsEmptyList()
        {
            //Arrange
            var user = new User()
            {
                ID = 1,
                Name = "user"
            };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(user);
            }

            var users = databaseManager.GetAllUsers();
            Assert.Equal(new List<User>() { user }, users);

            databaseManager.DeleteUser(user);
            users = databaseManager.GetAllUsers();
            Assert.Empty(users);
        }


        [Fact]
        public void DeleteUser_UserDoesntExistInTheDB_CantDeleteUser()
        {
            //Arrange
            var userToAdd = new User() { ID = 1 };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(userToAdd);
            }
            var userToDelete = new User() { ID = 2 };

            // Act
            bool deletionResult = databaseManager.DeleteUser(userToDelete);

            // Assert
            Assert.False(deletionResult);
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
            databaseManager.SaveTeam(team);

            List<Team> allTeams;
            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                allTeams = cnn.GetAll<Team>().ToList();
            }

            // Assert
            Assert.Contains(team, allTeams);
        }

        [Fact]
        public void GetAllTeams_SaveTeam_GetAllTeams_TeamsExistInDB_GetsAllTeams()
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

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(team1);
            }

            // Act
            var actualTeams = databaseManager.GetAllTeams();

            // Assert
            Assert.Equal(actualTeams, new List<Team>() { team1 });

            databaseManager.SaveTeam(team2);

            actualTeams = databaseManager.GetAllTeams();

            Assert.Equal(actualTeams, new List<Team>() { team1, team2 });
        }

        [Fact]
        public void GetAllTeams_DeleteTeam_GetAllTeams_TeamsExistInDB_GetsAllTeams()
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

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(team1);
                cnn.Insert(team2);
            }

            // Act
            var actualTeams = databaseManager.GetAllTeams();

            // Assert
            Assert.Equal(actualTeams, new List<Team>() { team1, team2 });

            databaseManager.DeleteTeam(team2);

            actualTeams = databaseManager.GetAllTeams();

            Assert.Equal(actualTeams, new List<Team>() { team1 });
        }
        [Fact]
        public void GetAllTeams_NoTeamExistsInTheDB_ReturnsEmptyList()
        {
            // Act
            var savedTeams = databaseManager.GetAllTeams();

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

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Act
            var deletionResult = databaseManager.DeleteTeam(team);

            // Assert
            Assert.True(deletionResult);
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInTheDB_CantDeleteTeam()
        {
            //Arrange
            var team = new Team()
            {
                ID = 1,
                Name = "team",
                CreationDate = "1234"
            };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(team);
            }

            // Act
            // Object in the DB has ID 1
            team.ID = 0;
            var deletionResult = databaseManager.DeleteTeam(team);

            // Assert
            Assert.False(deletionResult);
        }

        [Fact]
        public void GetAllUserIDToTeamID_NoUserIDToTeamIDExistsInTheDB_ReturnsEmptyList()
        {
            // Act
            var savedTeams = databaseManager.GetAllUserIDToTeamID();

            // Assert
            Assert.Empty(savedTeams);
        }

        [Fact]
        public void UserIDToTeamIDOperations()
        {
            //Arrange
            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID()
            {
                ID = 1,
                TeamID = 1,
                UserID = 1
            };

            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID()
            {
                ID = 2,
                TeamID = 1,
                UserID = 2
            };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID1);
                cnn.Insert(userIDToTeamID2);
            }

            var actualUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 }, actualUserIDToTeamIDs);

            var deletionResult = databaseManager.DeleteUserIDToTeamID(userIDToTeamID1);
            Assert.True(deletionResult);

            actualUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID2 }, actualUserIDToTeamIDs);

            // When it's added to DB db auto increments ID for the next added element
            userIDToTeamID1.ID = 3;
            databaseManager.SaveUserIDToTeamID(userIDToTeamID1);
            actualUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID2, userIDToTeamID1 }, actualUserIDToTeamIDs);
        }

        [Fact]
        public void SaveUserIDToTeamID_EmptyDB_SavesUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            // Act
            databaseManager.SaveUserIDToTeamID(userIDToTeamID);

            // Assert
            List<UserIDToTeamID> actualUserIDToTeamIDs;

            using (IDbConnection cnn = CreateConnection(connectionString))
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

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            bool deletionResult = databaseManager.DeleteUserIDToTeamID(userIDToTeamID);

            // Assert
            Assert.True(deletionResult);
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDDoesntExistInDB_CantDeleteUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (IDbConnection cnn = CreateConnection(connectionString))
            {
                cnn.Insert(userIDToTeamID);
            }

            // Act
            userIDToTeamID.ID = 3;
            bool deletionResult = databaseManager.DeleteUserIDToTeamID(userIDToTeamID);

            // Assert
            Assert.False(deletionResult);
        }


        protected abstract IDbConnection CreateConnection(string connectionString);

        protected abstract ManagerDatabaseController CreateDatabaseManager(string connectionString);

        protected abstract DatabaseTestHelperBase CreateDatabaseHelperMethods();

        public void Dispose()
        {
            databaseTestHelperMethods.DeleteCreatedDB();
        }
    }
}
