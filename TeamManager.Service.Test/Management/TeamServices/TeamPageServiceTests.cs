using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management
{
    public class TeamPageServiceTests
    {
        Mock<IManagementDatabaseConnection> connection;
        TeamPageService teamPageService;

        public TeamPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>());
            teamPageService = new TeamPageService(connection.Object);
        }

        [Fact]
        public void GetAllTeams_TeamsExistInDB_GetsTeams()
        {
            // Arrange
            List<Team> expectedTeams = new List<Team>()
            {
                new Team()
                {
                    Name ="Team1"
                },
                new Team()
                {
                    Name ="Team2"
                },
            };

            connection.Setup(x => x.GetAllTeams()).Returns(expectedTeams);

            // Act
            List<Team> actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Equal(expectedTeams, actualTeams);
        }
        [Fact]
        public void GetAllTeams_TeamsDontExistInDB_ReturnsEmptyList()
        {
            // Arrange
            connection.Setup(x => x.GetAllTeams()).Returns(new List<Team>());

            // Act
            List<Team> actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Empty(actualTeams);
        }

        [Fact]
        public void DeleteTeam_TeamExistInDB_DeletesTeam()
        {
            // Arrange
            Team expectedTeamToDelete = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            connection.Setup(c => c.DeleteTeam(It.Is<Team>(t => t == expectedTeamToDelete))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            // Act 
            teamPageService.DeleteTeam(expectedTeamToDelete);

            // Assert
            connection.Verify(c => c.DeleteTeam(It.Is<Team>(actualTeamToDelete => actualTeamToDelete.Equals(expectedTeamToDelete))));
        }

        [Fact]
        public void DeleteTeam_TeamHasMembers_ThrowsException()
        {
            // Arrange
            Team team = new Team()
            {
                ID = 1,
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { TeamID = 1, UserID = 1 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(team));
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInDB_ThrowsException()
        {
            // Arrange
            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            connection.Setup(c => c.DeleteTeam(It.Is<Team>(t => t == team))).Returns(false);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(team));
        }
    }
}