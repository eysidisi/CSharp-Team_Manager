using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management.UserServices
{
    public class UserDetailsPageServiceTests
    {
        [Fact]
        public void GetTeamsThatUserIn_OneTeamInDB_GetsTeams()
        {
            // Arange 
            var connection = new Mock<IManagerDatabaseConnection>();

            var userDetailsPageService = new UserDetailsPageService(connection.Object);

            User user1 = new User()
            {
                ID = 1
            };

            Team team1 = new Team() { ID = 1, Name = "team1" };
            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = team1.ID, UserID = user1.ID };

            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1 });
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID1 });

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);

            // Assert
            Assert.Single(teamsThatUser1IsIn);
            Assert.Contains(teamsThatUser1IsIn, t => t.ID == team1.ID);
        }

        [Fact]
        public void GetTeamsThatUserIn_NoTeamExists_GetsEmptyList()
        {
            // Arange 
            var connection = new Mock<IManagerDatabaseConnection>();

            var userDetailsPageService = new UserDetailsPageService(connection.Object);

            User user1 = new User()
            {
                ID = 1
            };

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = user1.ID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID1 });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>());

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
        }

        [Fact]
        public void GetUsersTeams_UserIsNotInAnyTeams_GetsEmptyList()
        {
            // Arange 
            var connection = new Mock<IManagerDatabaseConnection>();

            var userDetailsPageService = new UserDetailsPageService(connection.Object);

            User user1 = new User()
            {
                ID = 1
            };

            User user2 = new User()
            {
                ID = 2
            };

            Team team1 = new Team() { ID = 1, Name = "team1" };
            Team team2 = new Team() { ID = 2, Name = "team2" };

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = team1.ID, UserID = user2.ID };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { ID = 2, TeamID = team2.ID, UserID = user2.ID };
            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
        }
    }
}
