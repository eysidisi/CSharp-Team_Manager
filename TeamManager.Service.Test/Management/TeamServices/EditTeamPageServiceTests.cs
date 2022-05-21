using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management.TeamServices
{
    public class EditTeamPageServiceTests
    {
        Mock<IManagementDatabaseConnection> connection;
        public EditTeamPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
        }

        [Fact]
        public void AddUserToTheTeam_DBHasOtherUsersAddedToTeams_AddsUser()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team1", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };

            User userInTeamToEdit = new User() { ID = 2, Name = "user", CreationDate = "1234" };
            User userToAddToTeamToEdit = new User() { ID = 1, Name = "user", CreationDate = "1234" };
            User userInTeam2 = new User() { ID = 3, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userInTeam1IDToTeam1ID = new UserIDToTeamID() { ID = 1, UserID = userInTeamToEdit.ID, TeamID = teamToEdit.ID };
            UserIDToTeamID userInTeam2IDToTeam2ID = new UserIDToTeamID() { ID = 2, UserID = userInTeam2.ID, TeamID = team2.ID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userInTeam1IDToTeam1ID, userInTeam2IDToTeam2ID });
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { userToAddToTeamToEdit, userInTeamToEdit, userInTeam2 });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { teamToEdit, team2 });

            UserIDToTeamID expectedUserToTeamID = new UserIDToTeamID() { ID = 0, TeamID = teamToEdit.ID, UserID = userToAddToTeamToEdit.ID };

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act 
            editTeamPageService.AddUserToTheTeam(userToAddToTeamToEdit);

            // Assert
            connection.Verify(c =>
            c.SaveUserIDToTeamID(It.Is<UserIDToTeamID>(actualUserToTeamID =>
            actualUserToTeamID.Equals(expectedUserToTeamID))));
        }

        [Fact]
        public void AddUserToTheTeam_NoEntryInDB_ThrowsException()
        {
            // Arrange
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());

            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd));
        }

        [Fact]
        public void AddUserToTheTeam_UserIsAlreadyInTheTeam_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToAdd.ID, TeamID = teamToEdit.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User> { userToAdd });
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { teamToEdit });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd));
        }

        [Fact]
        public void AddUserToTheTeam_UserExistsInDBTeamDoesNot_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User> { userToAdd });
            connection.Setup(c => c.GetAllTeams()).Returns(Enumerable.Empty<Team>().ToList());
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(Enumerable.Empty<UserIDToTeamID>().ToList());

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd));
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsInTheTeam_RemovesUser()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = teamToEdit.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<User> users = new List<User>() { userToRemove };
            List<Team> teams = new List<Team>() { teamToEdit };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(users);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);

            UserIDToTeamID expectedUserToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToEdit.ID, UserID = userToRemove.ID };
            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act
            editTeamPageService.RemoveUserFromTheTeam(userToRemove);

            // Assert
            connection.Verify(c =>
            c.DeleteUserIDToTeamID(It.Is<UserIDToTeamID>(actualUserToTeamID =>
            actualUserToTeamID.Equals(expectedUserToTeamID))));
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsNotInTheTeam_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<User> users = new List<User>() { userToRemove };
            List<Team> teams = new List<Team>() { teamToEdit, team2 };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);
            connection.Setup(c => c.GetAllUsers()).Returns(users);

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            editTeamPageService.RemoveUserFromTheTeam(userToRemove));
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsNotInTheDB_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<Team> teams = new List<Team>() { teamToEdit, team2 };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);
            connection.Setup(c => c.GetAllUsers()).Returns(Enumerable.Empty<User>().ToList());

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            editTeamPageService.RemoveUserFromTheTeam(userToRemove));
        }

        [Fact]
        public void RemoveUserFromTheTeam_TeamIsNotInTheDB_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<User> users = new List<User>() { userToRemove };

            connection.Setup(c => c.GetAllTeams()).Returns(Enumerable.Empty<Team>().ToList());
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);
            connection.Setup(c => c.GetAllUsers()).Returns(users);

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            editTeamPageService.RemoveUserFromTheTeam(userToRemove));
        }

        [Fact]
        public void GetUsers_UsersExistInDB_ReturnsUsers()
        {
            // Arrange
            User user1 = new User() { ID = 1, Name = "User1" };
            User user2 = new User() { ID = 2, Name = "User2" };
            List<User> expectedUsers = new List<User>() { user1, user2 };

            connection.Setup(c => c.GetAllUsers()).Returns(expectedUsers);

            var editTeamPageService = new EditTeamPageService(connection.Object, new Team());
            // Act 
            var actualUsers = editTeamPageService.GetAllUsers();

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void TryToGetUsersInTeam_TeamHasUsers_ReturnsUsers()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = user.ID, TeamID = teamToEdit.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { teamToEdit, team2 });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act 
            var actualUsers = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Equal(new List<User>() { user }, actualUsers);
        }

        [Fact]
        public void TryToGetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = user.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { teamToEdit, team2 });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act 
            var users = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void TryToGetUsersInTeam_TeamIsNotInDB_ThrowsException()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = user.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team2 });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.TryToGetUsersInTheTeam());
        }

        [Fact]
        public void TryToGetUsersInTeam_NoUserExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { teamToEdit, team2 });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act 
            var users = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void TryToGetUsersInTeam_NoUserIsInTeams_ReturnsEmptyList()
        {
            // Arrange
            Team teamToEdit = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "asda" };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { teamToEdit, team2 });

            var editTeamPageService = new EditTeamPageService(connection.Object, teamToEdit);

            // Act 
            var users = editTeamPageService.TryToGetUsersInTheTeam();

            // Assert
            Assert.Empty(users);
        }
    }
}
