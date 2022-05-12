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
        EditTeamPageService editTeamPageService;
        public EditTeamPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            editTeamPageService = new EditTeamPageService(connection.Object);
        }

        [Fact]
        public void AddUserToTheTeam_DBHasOtherUsersAddedToTeams_AddsUser()
        {
            // Arrange
            Team team1 = new Team() { ID = 1, Name = "team1", CreationDate = "1234" };
            User user1InTeam1 = new User() { ID = 2, Name = "user", CreationDate = "1234" };
            User userToAddToTeam1 = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user2InTeam2 = new User() { ID = 3, Name = "user", CreationDate = "1234" };

            UserIDToTeamID user1IDToTeam1ID = new UserIDToTeamID() { ID = 1, UserID = user1InTeam1.ID, TeamID = team1.ID };
            UserIDToTeamID user2IDToTeam2ID = new UserIDToTeamID() { ID = 2, UserID = user2InTeam2.ID, TeamID = team2.ID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { user1IDToTeam1ID, user2IDToTeam2ID });
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { userToAddToTeam1, user1InTeam1, user2InTeam2 });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { team1, team2 });

            UserIDToTeamID expectedUserToTeamID = new UserIDToTeamID() { ID = 0, TeamID = team1.ID, UserID = userToAddToTeam1.ID };

            // Act 
            editTeamPageService.AddUserToTheTeam(userToAddToTeam1, team1);

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

            Team teamToAdd = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd, teamToAdd));
        }

        [Fact]
        public void AddUserToTheTeam_UserIsAlreadyInTheTeam_ThrowsException()
        {
            // Arrange
            Team teamToAdd = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToAdd.ID, TeamID = teamToAdd.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User> { userToAdd });
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { teamToAdd });

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd, teamToAdd));
        }

        [Fact]
        public void AddUserToTheTeam_UserExistsInDBTeamDoesNot_ThrowsException()
        {
            // Arrange
            Team teamToAddTheUser = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User> { userToAdd });
            connection.Setup(c => c.GetAllTeams()).Returns(Enumerable.Empty<Team>().ToList());
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(Enumerable.Empty<UserIDToTeamID>().ToList());

            // Act && Assert
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(userToAdd, teamToAddTheUser));
        }


        [Fact]
        public void RemoveUserFromTheTeam_UserIsInTheTeam_RemovesUser()
        {
            // Arrange
            Team teamToRemoveFrom = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = teamToRemoveFrom.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<User> users = new List<User>() { userToRemove };
            List<Team> teams = new List<Team>() { teamToRemoveFrom };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(users);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);

            UserIDToTeamID expectedUserToTeamID = new UserIDToTeamID() { ID = 1, TeamID = teamToRemoveFrom.ID, UserID = userToRemove.ID };
            
            // Act
            editTeamPageService.RemoveUserFromTheTeam(userToRemove, teamToRemoveFrom);

            // Assert
            connection.Verify(c => 
            c.DeleteUserIDToTeamID(It.Is<UserIDToTeamID>(actualUserToTeamID => 
            actualUserToTeamID.Equals(expectedUserToTeamID))));
        }

        [Fact]
        public void RemoveUserFromTheTeam_UserIsNotInTheTeam_ThrowsException()
        {
            // Arrange
            Team teamToRemoveFrom = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User userToRemove = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToRemove.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };
            List<User> users = new List<User>() { userToRemove };
            List<Team> teams = new List<Team>() { teamToRemoveFrom };

            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.DeleteUserIDToTeamID(userIDToTeamID)).Returns(true);
            connection.Setup(c => c.GetAllUsers()).Returns(users);

            // Act && Assert
            Assert.Throws<ArgumentException>(() =>
            editTeamPageService.RemoveUserFromTheTeam(userToRemove, teamToRemoveFrom));
        }

        [Fact]
        public void GetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            Team team1 = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = user.ID, TeamID = team2.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act 
            var users = editTeamPageService.GetUsersInTeam(team1);

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GetUsersInTeam_NoUserExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            Team team1 = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act 
            var users = editTeamPageService.GetUsersInTeam(team1);

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GetUsersInTeam_NoUserIsInTeams_ReturnsEmptyList()
        {
            // Arrange
            Team team1 = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user = new User() { ID = 1, Name = "asda" };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>() { user });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act 
            var users = editTeamPageService.GetUsersInTeam(team1);

            // Assert
            Assert.Empty(users);
        }

    }
}
