using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.TeamServices
{
    public abstract class TeamPageServiceTests : IntegrationTests
    {
        readonly TeamPageService teamPageService;
        public TeamPageServiceTests()
        {
            teamPageService = new TeamPageService(databaseController);
        }

        [Fact]
        public void GetAllTeams_DBHasNoTeams_ReturnsEmptyList()
        {
            // Act
            var teams = teamPageService.GetAllTeams();

            // Assert
            Assert.Empty(teams);
        }

        [Fact]
        public void GetAllTeams_DBHasTeams_ReturnsTeams()
        {
            // Arrange
            List<Team> expectedTeams = new List<Team>()
            {
                new Team(){Name="team1",ID=1},
                new Team(){Name="team2",ID=2}
            };

            using (var cnn = CreateConnection(connString))
            {
                cnn.Insert(expectedTeams);
            }

            // Act
            var actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Equal(expectedTeams, actualTeams);
        }

        [Fact]
        public void DeleteTeam_DBHasNoTeams_DeletesTeam()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };

            using (var conn = CreateConnection(connString))
            {
                conn.Insert(teamToDelete);
            }

            // Act 
            teamPageService.DeleteTeam(teamToDelete);

            List<Team> actualTeams;
            using (var conn = CreateConnection(connString))
            {
                actualTeams = conn.GetAll<Team>().ToList();
            }

            Assert.Empty(actualTeams);
        }



        [Fact]

        public void DeleteTeam_DBHasNoTeams_ThrowsException()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }

        [Fact]
        public void DeleteTeam_DBDoesNotHaveThatTeam_ThrowsException()
        {
            // Arrange
            Team teamToAdd = new Team() { Name = "teamToAdd", ID = 1 };
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 2 };

            using (var cnn = CreateConnection(connString))
            {
                cnn.Insert(teamToAdd);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }

        [Fact]
        public void DeleteTeam_TeamHasUsers_ThrowsException()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = 1 };

            using (var cnn = CreateConnection(connString))
            {
                cnn.Insert(teamToDelete);
                cnn.Insert(userIDToTeamID);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }
    }
}
