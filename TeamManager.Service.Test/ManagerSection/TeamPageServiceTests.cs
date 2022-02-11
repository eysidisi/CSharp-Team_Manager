using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class TeamPageServiceTests
    {
        [Fact]
        public void GetTeams_GetsTeams()
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
        public void DeleteTeam_DeletesTeam()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            // Act
            teamPageService.DeleteTeam(team);

            // Assert
        }
    }
}
