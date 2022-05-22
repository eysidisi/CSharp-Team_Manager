using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class EditTeamPageServiceTests : SQLiteIntegrationTestsBase
    {
        readonly Team teamToEdit;
        EditTeamPageService editTeamPageService;

        public EditTeamPageServiceTests()
        {
            teamToEdit = new Team() { Name = "team", ID = 1 };
            editTeamPageService = new EditTeamPageService(databaseManager, teamToEdit);
        }

        [Fact]
        public void AddUserToTheTeam_UserAndTeamExists_AddsUserToTheTeam()
        {
            // Arrange
            User user = new User() { Name = "user", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToEdit);
                cnn.Insert(user);
            }

            // Act
            editTeamPageService.AddUserToTheTeam(user);

            // Assert
            List<UserIDToTeamID> userIDToTeamIDs;

            using (var cnn = new SQLiteConnection(connString))
            {
                userIDToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.Contains(userIDToTeamIDs, u => u.UserID == teamToEdit.ID && u.TeamID == teamToEdit.ID);
        }

        [Fact]
        public void AddUserToTheTeam_NoTeamOrUserExistsInDB_ThrowsException()
        {
            // Arrange
            User user = new User() { Name = "user", ID = 1 };


            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user));
        }

        [Fact]
        public void AddUserToTheTeam_TeamExistUserDoesntInDB_ThrowsException()
        {
            // Arrange
            User user = new User() { Name = "user", ID = 1 };


            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(teamToEdit);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user));
        }

        [Fact]
        public void AddUserToTheTeam_UserExistTeamDoesntInDB_ThrowsException()
        {
            // Arrange
            User user = new User() { Name = "user", ID = 1 };


            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user));
        }

        [Fact]
        public void AddUserToTheTeam_UserIsAlreadyInTheTeam_ThrowsException()
        {
            // Arrange
            User user = new User() { Name = "user", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
                con.Insert(teamToEdit);
                con.Insert(userIDToTeamID);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user));
        }

        [Fact]
        public void GetUsers_UsersExistInDB_ReturnsUsers()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };
            User user2 = new User() { Name = "user2", ID = 2 };
            List<User> expecteUsers = new List<User>() { user1, user2 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(expecteUsers);
            }

            // Act
            var actualUsers = editTeamPageService.GetAllUsers();

            // Assert
            Assert.Equal(expecteUsers, actualUsers);
        }

        [Fact]
        public void GetUsers_NoUserExistsInDB_ReturnsEmptyList()
        {
            // Arrange

            // Act
            var actualUsers = editTeamPageService.GetAllUsers();

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasUsers_ReturnsUsers()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };
            User user2 = new User() { Name = "user2", ID = 2 };

            List<User> expectedUsers = new List<User>() { user1, user2 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { TeamID = teamToEdit.ID, UserID = user1.ID };
            UserIDToTeamID user2IDToTeamID = new UserIDToTeamID() { TeamID = teamToEdit.ID, UserID = user2.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1);
                con.Insert(user2);
                con.Insert(teamToEdit);
                con.Insert(user1IDToTeamID);
                con.Insert(user2IDToTeamID);
            }

            // Act
            var actualUsers = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            User user = new User() { Name = "user1", ID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
                con.Insert(teamToEdit);
            }

            // Act
            var actualUsers = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_TeamDoesntExistInDB_ReturnsEmptyList()
        {
            // Arrange
            User user = new User() { Name = "user1", ID = 1 };
            Team teamExists = new Team() { Name = "teamExists", ID = 1 };
            Team teamDoesNotExist = new Team() { Name = "teamDoesNotExists", ID = 2 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
                con.Insert(teamExists);
            }

            editTeamPageService = new EditTeamPageService(databaseManager, teamDoesNotExist);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.TryToGetUsersInTheTeam());
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsInTheTeam_RemovesUser()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };
            User user2 = new User() { Name = "user2", ID = 2 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToEdit.ID, UserID = user1.ID };
            UserIDToTeamID user2IDToTeamID = new UserIDToTeamID() { ID = 2, TeamID = teamToEdit.ID, UserID = user2.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(teamToEdit);
                con.Insert(user1);
                con.Insert(user2);
                con.Insert(user1IDToTeamID);
                con.Insert(user2IDToTeamID);
            }

            // Act 
            editTeamPageService.RemoveUserFromTheTeam(user1);

            // Assert
            List<UserIDToTeamID> actualUserToTeamIDs;
            using (var con = new SQLiteConnection(connString))
            {
                actualUserToTeamIDs = con.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.DoesNotContain(user1IDToTeamID, actualUserToTeamIDs);
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsNotInTheTeam_ThrowsException()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };
            User user2 = new User() { Name = "user2", ID = 2 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToEdit.ID, UserID = user1.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(teamToEdit);
                con.Insert(user1IDToTeamID);
                con.Insert(user1);
                con.Insert(user2);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.RemoveUserFromTheTeam(user2));
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserDoesntExistInDB_ThrowsException()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToEdit.ID, UserID = user1.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(teamToEdit);
                con.Insert(user1IDToTeamID);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.RemoveUserFromTheTeam(user1));
        }

        [Fact]
        public void RemoveUserFromTheTeam_TeamDoesntExistInDB_ThrowsException()
        {
            // Arrange
            User user1 = new User() { Name = "user1", ID = 1 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToEdit.ID, UserID = user1.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1IDToTeamID);
                con.Insert(user1);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.RemoveUserFromTheTeam(user1));
        }

    }
}
