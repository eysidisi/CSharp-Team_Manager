﻿using Moq;
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

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            // Act && Assert
            teamPageService.DeleteTeam(team);
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
