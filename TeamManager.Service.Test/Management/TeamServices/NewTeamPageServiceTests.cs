using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using Xunit;
using Moq;
using TeamManager.Service.Models;

namespace TeamManager.Service.Test.Management
{
    public class NewTeamPageServiceTests
    {
        Mock<IManagementDatabaseConnection> connection;
        NewTeamPageService newTeamPageService;

        public NewTeamPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            newTeamPageService = new NewTeamPageService(connection.Object);
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

            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { });

            // Act 
            newTeamPageService.SaveTeam(teamToSave);

            // Assert
            connection.Verify(c => c.SaveTeam(It.Is<Team>(actualSavedTeam => actualSavedTeam.Equals(teamToSave))));
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
            
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { newTeam });

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
