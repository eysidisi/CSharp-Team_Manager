using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management
{
    public class TeamDetailsPageServiceTests
    {
        Mock<IManagementDatabaseConnection> connection;
        TeamDetailsPageService teamDetailsPage;
        Team teamToGetDetails;

        public TeamDetailsPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            teamToGetDetails = new Team() { ID = 1, Name = "Team1" };
            teamDetailsPage = new TeamDetailsPageService(connection.Object,teamToGetDetails);
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

            connection.Setup(c => c.GetAllUsers()).Returns(users);
            connection.Setup(c => c.GetAllTeams()).Returns(teams);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);

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
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());

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

            connection.Setup(c => c.GetAllUsers()).Returns(users);
            connection.Setup(c => c.GetAllTeams()).Returns(teams);

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam();

            // Assert
            Assert.Empty(actualUsers);
        }
    }
}
