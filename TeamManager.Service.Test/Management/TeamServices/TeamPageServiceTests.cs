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
        [Fact]
        public void GetAllTeams_TeamsExistInDB_GetsTeams()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

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

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act
            List<Team> actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Equal(expectedTeams, actualTeams);
        }
        [Fact]
        public void GetAllTeams_TeamsDontExistInDB_ReturnsEmptyList()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

            connection.Setup(x => x.GetAllTeams()).Returns(new List<Team>());

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act
            List<Team> actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Empty(actualTeams);
        }

        [Fact]
        public void DeleteTeam_TeamExistInDB_DeletesTeam()
        {
            // Arrange
            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.DeleteTeam(It.Is<Team>(t => t == team))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act && Assert
            teamPageService.DeleteTeam(team);
        }

        [Fact]
        public void DeleteTeam_TeamHasMembers_DeletesTeam()
        {
            // Arrange
            Team team = new Team()
            {
                ID = 1,
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { TeamID = 1, UserID = 1 };

            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(team));
        }

        [Fact]
        public void DeleteTeam_TeamDoesntExistInDB_CantDeleteTeam()
        {
            // Arrange
            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.DeleteTeam(It.Is<Team>(t => t == team))).Returns(false);

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(team));
        }
    }
}
