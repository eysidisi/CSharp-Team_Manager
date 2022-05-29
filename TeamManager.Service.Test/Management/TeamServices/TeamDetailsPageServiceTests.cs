using System.Collections.Generic;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class TeamDetailsPageServiceTests : TeamServiceTestsBase
    {
        readonly TeamDetailsPageService teamDetailsPage;
        readonly Team teamToGetDetails;

        public TeamDetailsPageServiceTests()
        {
            teamToGetDetails = new Team() { ID = 1, Name = "Team1" };
            teamDetailsPage = new TeamDetailsPageService(databaseController.Object, teamToGetDetails);
        }

        [Fact]
        public void GetUsersInTheTeam_TeamsExistInDB_GetsUsers()
        {
            // Arrange
            User user1 = new User() { ID = 1, Name = "ali" };
            User user2 = new User() { ID = 2, Name = "veli" };
            Team team2 = new Team() { ID = 2, Name = "Team2" };
            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { UserID = user1.ID, TeamID = teamToGetDetails.ID, };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { UserID = user2.ID, TeamID = team2.ID, };

            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };
            List<User> users = new List<User>() { user1, user2 };
            List<Team> teams = new List<Team>() { teamToGetDetails, team2 };

            databaseController.Setup(c => c.GetAllUsers()).Returns(users);
            databaseController.Setup(c => c.GetAllTeams()).Returns(teams);
            databaseController.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);

            List<User> expectedUsers = new List<User>() { user1 };

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam();

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void GetUsersInTheTeam_NoUsersInTheDB_ReturnsEmptyList()
        {
            // Arrange
            databaseController.Setup(c => c.GetAllUsers()).Returns(new List<User>());
            databaseController.Setup(c => c.GetAllTeams()).Returns(new List<Team>());
            databaseController.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam();

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTheTeam_NoUsersInTheTeam_ReturnsEmptyList()
        {
            // Arrange
            User user1 = new User() { Name = "ali" };
            User user2 = new User() { Name = "veli" };

            List<User> users = new List<User>() { user1, user2 };
            List<Team> teams = new List<Team>() { teamToGetDetails };

            databaseController.Setup(c => c.GetAllUsers()).Returns(users);
            databaseController.Setup(c => c.GetAllTeams()).Returns(teams);
            databaseController.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam();

            // Assert
            Assert.Empty(actualUsers);
        }
    }
}
