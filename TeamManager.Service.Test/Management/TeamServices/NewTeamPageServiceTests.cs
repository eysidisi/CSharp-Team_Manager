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
        [Fact]
        public void SaveTeam_EmptyDB_SavesTeam()
        {
            // Arrange
            Team newTeam = new Team()
            {
                Name = "Team1",
                CreationDate = "123"
            };
            var connection = new Mock<IManagementDatabaseConnection>();
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team> { });
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act && Assert
            newTeamPageService.SaveTeam(newTeam);
        }

        [Fact]
        public void SaveTeam_TeamAlreadyExists_CantSaveTeam()
        {
            // Arrange
            Team newTeam = new Team()
            {
                Name = "Team1",
                CreationDate = "123"
            };
            var connection = new Mock<IManagementDatabaseConnection>();
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { newTeam });

            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(newTeam));
        }

        [Fact]
        public void SaveTeam_InvalidTeam_CantSaveTeam()
        {
            // Arrange
            Team newTeam = new Team()
            {
                CreationDate = "123"
            };
            var connection = new Mock<IManagementDatabaseConnection>();

            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(newTeam));
        }
    }
}
