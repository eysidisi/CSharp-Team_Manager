using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.TeamServices
{
    public abstract class NewTeamPageServiceTests : IntegrationTests
    {
        readonly NewTeamPageService newTeamPageService;
        public NewTeamPageServiceTests()
        {
            newTeamPageService = new NewTeamPageService(databaseController);
        }

        [Fact]
        public void SaveTeam_NoTeamExistsInDB_AddsTeam()
        {
            // Arrange
            Team team = new Team() { Name = "Team1", ID = 1 };

            // Act
            newTeamPageService.SaveTeam(team);

            // Assert
            List<Team> actualTeams;

            using (var cnn = CreateConnection(connString))
            {
                actualTeams = cnn.GetAll<Team>().ToList();
            }

            Assert.Contains(team, actualTeams);
        }

        [Fact]
        public void SaveTeam_TeamWithSameNameExistsInDB_ThrowsException()
        {
            // Arrange
            Team team = new Team() { Name = "Team1" };

            using (var cnn = CreateConnection(connString))
            {
                cnn.Insert(team);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(team));
        }

        [Fact]
        public void SaveTeam_InvalidTeam_ThrowsException()
        {
            // Arrange
            Team team = new Team() { };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(team));
        }
    }
}
