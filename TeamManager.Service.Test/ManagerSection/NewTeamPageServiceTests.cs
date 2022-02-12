using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using Xunit;
using Moq;
using TeamManager.Service.Models;

namespace TeamManager.Service.Test.ManagerSection
{
    public class NewTeamPageServiceTests
    {
        [Fact]
        public void SaveTeam_SavesTeam()
        {
            // Arrange
            Team newTeam = new Team()
            {
                Name = "Team1",
                CreationDate = "123"
            };
            var connection = new Mock<IManagerDatabaseConnection>();
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act
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
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetTeamWithName("Team1")).Returns(newTeam);

            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act
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
            var connection = new Mock<IManagerDatabaseConnection>();

            NewTeamPageService newTeamPageService = new NewTeamPageService(connection.Object);

            // Act
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(newTeam));
        }
    }
}
