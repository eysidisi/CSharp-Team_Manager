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
        Mock<IManagementDatabaseConnection> connection;
        UserDetailsPageService userDetailsPageService;
        User userToGetDetails;
        public UserDetailsPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            userToGetDetails = new User() { ID = 1 };
            userDetailsPageService = new UserDetailsPageService(connection.Object, userToGetDetails);
        }

        [Fact]
        public void GetTeamsThatUserIn_OneTeamInDB_GetsTeams()
        {
            // Arange 
            Team team = new Team() { ID = 1, Name = "team1" };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = team.ID, UserID = userToGetDetails.ID };

            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team });
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            List<Team> expectedTeamsUserIsIn = new List<Team>() { team };

            // Act
            List<Team> actualTeamsUserIsIn = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Equal(expectedTeamsUserIsIn, actualTeamsUserIsIn);
        }

        [Fact]
        public void GetTeamsThatUserIn_NoTeamExists_GetsEmptyList()
        {
            // Arange 
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = userToGetDetails.ID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>());

            // Act
            List<Team> teamsThatUserIsIn = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Empty(teamsThatUserIsIn);
        }

        [Fact]
        public void GetTeamsThatUserIn_NoUserIDToTeamIDExists_GetsEmptyList()
        {
            // Arange 
            User user2 = new User() { ID = 2 };

            Team team1 = new Team() { ID = 1, Name = "team1" };
            Team team2 = new Team() { ID = 2, Name = "team2" };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
        }

        [Fact]
        public void GetTeamsThatUserIn_UserIsNotInAnyTeams_GetsEmptyList()
        {
            // Arange 
            User user2 = new User() { ID = 2 };

            Team team1 = new Team() { ID = 1, Name = "team1" };
            Team team2 = new Team() { ID = 2, Name = "team2" };

            UserIDToTeamID userIDToTeamID1 = new UserIDToTeamID() { ID = 1, TeamID = team1.ID, UserID = user2.ID };
            UserIDToTeamID userIDToTeamID2 = new UserIDToTeamID() { ID = 2, TeamID = team2.ID, UserID = user2.ID };
            List<UserIDToTeamID> userIDToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID1, userIDToTeamID2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDToTeamIDs);
            connection.Setup(c => c.GetAllTeams()).Returns(new List<Team>() { team1, team2 });

            // Act
            List<Team> teamsThatUser1IsIn = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Empty(teamsThatUser1IsIn);
        }
    }
}
