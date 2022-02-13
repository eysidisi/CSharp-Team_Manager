using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.ManagerSection.UserServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection.UserServices
{
    public class UserDetailsPageServiceTests
    {
        [Fact]
        public void GetUsersTeams_GetsTeams()
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
            List<Team> teams = new List<Team>() { team1, team2 };

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = team1.ID, UserID = user1.ID };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { ID = 2, TeamID = team2.ID, UserID = user2.ID };
            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(teams);

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);
            List<Team> teamsThatUser2IsIn = userDetailsPageService.GetTeamsThatUserIn(user2);

            // Assert
            Assert.Single(teamsThatUser1IsIn);
            Assert.Contains(teamsThatUser1IsIn, t => t.ID == team1.ID);

            Assert.Single(teamsThatUser2IsIn);
            Assert.Contains(teamsThatUser2IsIn, t => t.ID == team2.ID);
        }

        [Fact]
        public void GetUsersTeams_NoTeamExists_GetsEmptyList()
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

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = user1.ID };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { ID = 2, TeamID = 1, UserID = user2.ID };
            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>());

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);
            List<Team> teamsThatUser2IsIn = userDetailsPageService.GetTeamsThatUserIn(user2);

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
            Assert.Empty(teamsThatUser2IsIn);
        }
        [Fact]
        public void GetUsersTeams_UserIsInNoTeams_GetsEmptyList()
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
            List<Team> teams = new List<Team>() { team1, team2 };

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = team1.ID, UserID = user2.ID };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { ID = 2, TeamID = team2.ID, UserID = user2.ID };
            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(teams);

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn(user1);

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
        }
    }
}
