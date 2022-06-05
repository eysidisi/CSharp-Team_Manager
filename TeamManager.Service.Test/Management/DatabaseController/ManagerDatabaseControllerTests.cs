using Dapper.Contrib.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TeamManager.Service.Management.DatabaseConnection;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.DatabaseControllers
{
    public class ManagerDatabaseControllerTests
    {
        readonly ManagerDatabaseController databaseController;
        Mock<IManagerDatabaseConnection> connection;

        public ManagerDatabaseControllerTests()
        {
            connection = new Mock<IManagerDatabaseConnection>();
            databaseController = new ManagerDatabaseController(connection.Object);
        }

        [Fact]
        public void GetAllUsers_EmptyDB_ReturnsEmptyList()
        {
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());

            var users = databaseController.GetAllUsers();

            Assert.Empty(users);
        }

        [Fact]
        public void GetAllUsers_SaveNewUser_GetAllUsers_UsersExistInTheDB_ReturnsUsers()
        {
            var userInDB = new User()
            {
                ID = 1,
                Name = "user1"
            };

            var userToAdd = new User()
            {
                ID = 2,
                Name = "user2"
            };

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { userInDB });
            connection.Setup(c => c.SaveUser(It.IsAny<User>()));

            var actualUsers = databaseController.GetAllUsers();

            Assert.Equal(new List<User>() { userInDB }, actualUsers);

            databaseController.SaveUser(userToAdd);

            actualUsers = databaseController.GetAllUsers();

            Assert.Equal(new List<User>() { userInDB, userToAdd }, actualUsers);
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

            connection.Setup(c => c.DeleteUser(user)).Returns(true);

            // Act
            bool deletionResult = databaseController.DeleteUser(user);

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

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });

            var users = databaseController.GetAllUsers();
            Assert.Equal(new List<User>() { user }, users);

            connection.Setup(c => c.DeleteUser(user)).Returns(true);
            databaseController.DeleteUser(user);
            users = databaseController.GetAllUsers();
            Assert.Empty(users);
        }

        [Fact]
        public void DeleteUser_UserDoesntExistInTheDB_CantDeleteUser()
        {
            //Arrange
            var userToDelete = new User() { ID = 2 };
            connection.Setup(c => c.DeleteUser(userToDelete)).Returns(false);

            // Act
            bool deletionResult = databaseController.DeleteUser(userToDelete);

            // Assert
            Assert.False(deletionResult);
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

            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1 });

            // Act
            var actualTeams = databaseController.GetAllTeams();

            // Assert
            Assert.Equal(actualTeams, new List<Team>() { team1 });

            databaseController.SaveTeam(team2);

            actualTeams = databaseController.GetAllTeams();

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

            List<Team> teams = new List<Team>() { team1, team2 };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.DeleteTeam(It.IsIn<Team>(teams))).Returns(true);

            // Act
            var actualTeams = databaseController.GetAllTeams();

            // Assert
            Assert.Equal(actualTeams, new List<Team>() { team1, team2 });

            Assert.True(databaseController.DeleteTeam(team2));

            actualTeams = databaseController.GetAllTeams();

            Assert.Equal(actualTeams, new List<Team>() { team1 });
        }

        [Fact]
        public void GetAllTeams_NoTeamExistsInTheDB_ReturnsEmptyList()
        {
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>());

            // Act
            var savedTeams = databaseController.GetAllTeams();

            // Assert
            Assert.Empty(savedTeams);
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
            var teamsInDB = new List<Team>() { team };
            connection.Setup(c => c.GetAllTeams()).Returns(teamsInDB);
            connection.Setup(c => c.DeleteTeam(It.IsIn<Team>(teamsInDB))).Returns(true);

            var teamDoesntExist = new Team()
            {
                ID = 0,
                Name = "team",
                CreationDate = "1234"
            };

            var deletionResult = databaseController.DeleteTeam(teamDoesntExist);

            // Assert
            Assert.False(deletionResult);
        }

        [Fact]
        public void GetAllUserIDToTeamID_NoUserIDToTeamIDExistsInTheDB_ReturnsEmptyList()
        {
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            // Act
            var savedTeams = databaseController.GetAllUserIDToTeamID();

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
            var allUserIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(allUserIDToTeamIDs);
            var actualUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 }, actualUserIDToTeamIDs);

            connection.Setup(c => c.DeleteUserIDToTeamID(It.IsIn<UserIDToTeamID>(allUserIDToTeamIDs))).Returns(true);
            var deletionResult = databaseController.DeleteUserIDToTeamID(userIDToTeamID1);
            Assert.True(deletionResult);

            actualUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID2 }, actualUserIDToTeamIDs);

            databaseController.SaveUserIDToTeamID(userIDToTeamID1);
            actualUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            Assert.Equal(new List<UserIDToTeamID>() { userIDToTeamID2, userIDToTeamID1 }, actualUserIDToTeamIDs);
        }

        [Fact]
        public void SaveUserIDToTeamID_EmptyDB_SavesUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            // Act
            databaseController.SaveUserIDToTeamID(userIDToTeamID);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            // Assert
            List<UserIDToTeamID> actualUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();

            Assert.Contains(userIDToTeamID, actualUserIDToTeamIDs);
        }

        [Fact]
        public void DeleteUserIDToTeamID_UserIDToTeamIDDoesntExistInDB_CantDeleteUserIDToTeamID()
        {
            //Arrange
            var userIDToTeamIDExists = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };
            var userIDToTeamIDDoesntExist = new UserIDToTeamID() { ID = 2, UserID = 1, TeamID = 1 };

            connection.Setup(c => c.DeleteUserIDToTeamID(It.Is<UserIDToTeamID>(u => u == userIDToTeamIDExists))).Returns(true);

            // Act
            bool deletionResult = databaseController.DeleteUserIDToTeamID(userIDToTeamIDDoesntExist);

            // Assert
            Assert.False(deletionResult);
        }
    }
}
