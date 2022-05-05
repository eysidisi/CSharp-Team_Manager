using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class EditTeamPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void AddUserToTheTeam_UserAndTeamExists_AddsUserToTheTeam()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
                cnn.Insert(user);
            }

            // Act
            editTeamPageService.AddUserToTheTeam(user, team);

            // Assert
            List<UserIDToTeamID> userIDToTeamIDs;

            using (var cnn = new SQLiteConnection(connString))
            {
                userIDToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.Contains(userIDToTeamIDs, u => u.UserID == team.ID && u.TeamID == team.ID);
        }

        [Fact]
        public void AddUserToTheTeam_NoTeamOrUserExistsInDB_CantAdd()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user, team));
        }

        [Fact]
        public void AddUserToTheTeam_TeamExistUserDoesntInDB_CantAdd()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(team);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user, team));
        }

        [Fact]
        public void AddUserToTheTeam_UserExistTeamDoesntInDB_CantAdd()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user, team));
        }

        [Fact]
        public void AddUserToTheTeam_UserIsAlreadyInTheTeam_CantAdd()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user);
                con.Insert(team);
                con.Insert(userIDToTeamID);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user, team));
        }

        [Fact]
        public void GetUsers_UsersExistInDB_ReturnsUsers()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);
            User user1 = new User() { Name = "user1" };
            User user2 = new User() { Name = "user2" };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1);
                con.Insert(user2);
            }

            // Act
            var actualUsers = editTeamPageService.GetUsers();

            // Assert
            Assert.Contains(actualUsers, u => u.Name == user1.Name);
            Assert.Contains(actualUsers, u => u.Name == user2.Name);
        }

        [Fact]
        public void GetUsers_NoUserExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            // Act && Assert
            Assert.Empty(editTeamPageService.GetUsers());
        }

        [Fact]
        public void GetUsersInTeam_TeamHasUsers_ReturnsUsers()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);
            User user1 = new User() { Name = "user1", ID = 1 };
            User user2 = new User() { Name = "user2", ID = 2 };

            List<User> expectedUsers = new List<User>() { user1, user2 };

            Team team = new Team() { Name = "team", ID = 1 };

            UserIDToTeamID user1IDToTeamID = new UserIDToTeamID() { TeamID = team.ID, UserID = user1.ID };
            UserIDToTeamID user2IDToTeamID = new UserIDToTeamID() { TeamID = team.ID, UserID = user2.ID };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1);
                con.Insert(user2);
                con.Insert(team);
                con.Insert(user1IDToTeamID);
                con.Insert(user2IDToTeamID);
            }

            // Act
            var actualUsers = editTeamPageService.GetUsersInTeam(team);

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);
            User user1 = new User() { Name = "user1", ID = 1 };

            Team team = new Team() { Name = "team", ID = 1 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1);
                con.Insert(team);
            }

            // Act
            var actualUsers = editTeamPageService.GetUsersInTeam(team);

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_TeamDoesntExistInDB_ReturnsEmptyList()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);
            User user1 = new User() { Name = "user1", ID = 1 };

            Team teamExists = new Team() { Name = "teamExists", ID = 1 };
            Team teamDoesntExist = new Team() { Name = "teamDoesntExist", ID = 2 };

            using (var con = new SQLiteConnection(connString))
            {
                con.Insert(user1);
                con.Insert(teamExists);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.GetUsersInTeam(teamDoesntExist));
        }
    }
}
