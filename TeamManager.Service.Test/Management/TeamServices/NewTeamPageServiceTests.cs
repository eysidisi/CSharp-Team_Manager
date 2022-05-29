using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class NewTeamPageServiceTests : TeamServiceTestsBase
    {
        readonly NewTeamPageService newTeamPageService;

        public NewTeamPageServiceTests()
        {
            newTeamPageService = new NewTeamPageService(databaseController.Object);
        }

        [Fact]
        public void SaveTeam_EmptyDB_SavesTeam()
        {
            // Arrange
            Team teamToSave = new Team()
            {
                ID = 1,
                Name = "Team1",
                CreationDate = "123"
            };

            databaseController.Setup(c => c.GetAllTeams()).Returns(new List<Team> { });

            // Act 
            newTeamPageService.SaveTeam(teamToSave);

            // Assert
            databaseController.Verify(c => c.SaveTeam(It.Is<Team>(actualSavedTeam => actualSavedTeam.Equals(teamToSave))));
        }

        [Fact]
        public void SaveTeam_TeamAlreadyExists_ThrowsException()
        {
            // Arrange
            Team newTeam = new Team()
            {
                Name = "Team1",
                CreationDate = "123"
            };

            databaseController.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { newTeam });

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(newTeam));
        }

        [Fact]
        public void SaveTeam_InvalidTeam_ThrowsException()
        {
            // Arrange
            Team newTeam = new Team()
            {
                CreationDate = "123"
            };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(newTeam));
        }
    }
}
