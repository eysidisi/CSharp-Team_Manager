using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class TeamDetailsPageServiceTests
    {
        [Fact]
        public void GetUsersInTheTeam_GetsUsers()
        {
            // Arrange
            User user1 = new User() { Name = "ali", TeamName = "Team1" };
            User user2 = new User() { Name = "veli", TeamName = "Team2" };
            Team team1 = new Team() { Name = "Team1" };

            List<User> users = new List<User>() { user1, user2 };
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetAllUsers()).Returns(users);
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(connection.Object);
            List<User> expectedUsers = new List<User>() { user1 };

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam(team1);

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void GetUsersInTheTeam_NoUsersInTheDB()
        {
            // Arrange
            Team team1 = new Team() { Name = "Team1" };

            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetAllUsers()).Returns<List<User>>(null);
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(connection.Object);
            List<User> expectedUsers = new List<User>();

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam(team1);

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }
        [Fact]
        public void GetUsersInTheTeam_NoUsersInTheTeam()
        {
            // Arrange
            User user1 = new User() { Name = "ali", TeamName = "Team2" };
            User user2 = new User() { Name = "veli", TeamName = "Team2" };
            Team team1 = new Team() { Name = "Team1" };

            List<User> users = new List<User>() { user1, user2 };
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetAllUsers()).Returns(users);
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(connection.Object);
            List<User> expectedUsers = new List<User>() ;

            // Act
            List<User> actualUsers = teamDetailsPage.GetUsersInTeam(team1);

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

    }
}
