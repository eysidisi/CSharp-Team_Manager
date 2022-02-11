using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.ManagerSection;
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
            var connection = new Mock<IDatabaseConnection>();

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
        public void AddTeam_AddsTeam()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            string teamName = "Team1";

            connection.Setup(x => x.GetTeamWithName(It.
                Is<string>(t => t == teamName))).Returns<Team>(null);

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = teamName,
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            // Act
            teamPageService.AddTeam(team);

            // Assert
        }
        [Fact]
        public void AddTeam_CantAddTeam()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            string teamName = "Team1";

            connection.Setup(x => x.GetTeamWithName(It.
                Is<string>(t => t == teamName))).Returns(new Team());

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = teamName,
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => teamPageService.AddTeam(team));
        }
        [Fact]
        public void DeleteTeam_DeletesTeam()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

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
        [Fact]
        public void AddUserToTeam_AddsUser()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            User user = new User()
            {
                ID = 1,
                Name = "Ali"
            };

            // Act
            teamPageService.AddUserToTheTeam(team, user);

            // Assert
        }
        [Fact]
        public void AddUserToTeam_CantAddUser()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void DeleteUserFromTeam_RemovesUser()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            User user = new User()
            {
                ID = 1,
                Name = "Ali"
            };

            // Act
            teamPageService.DeleteUserFromTheTeam(team, user);

            // Assert

        }
        [Fact]
        public void DeleteUserFromTeam_CantRemoveUser()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            User user = new User()
            {
                ID = 1,
                Name = "Ali"
            };

            // Act
            teamPageService.DeleteUserFromTheTeam(team, user);

            // Assert

        }
        [Fact]
        public void GetUsersInATeam()
        {
            // Arrange
            var connection = new Mock<IDatabaseConnection>();

            TeamPageService teamPageService = new TeamPageService(connection.Object);

            Team team = new Team()
            {
                Name = "Team1",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            User user = new User()
            {
                ID = 1,
                Name = "Ali"
            };

            // Act
            teamPageService.DeleteUserFromTheTeam(team, user);

            // Assert

        }
    }
}
