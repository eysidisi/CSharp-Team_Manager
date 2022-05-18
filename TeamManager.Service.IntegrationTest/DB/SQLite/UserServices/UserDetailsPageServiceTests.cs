using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class UserDetailsPageServiceTests : SQLiteIntegrationTestsBase
    {
        User userToGetDetails;
        UserDetailsPageService userDetailsPageService;
        public UserDetailsPageServiceTests()
        {
            userToGetDetails = new User() { ID = 1, Name = "UserToGetDetails" };
            userDetailsPageService = new UserDetailsPageService(connection, userToGetDetails);
        }

        [Fact]
        public void GetTeamsThatUserIn_EmptyDB_ReturnsEmptyList()
        {
            // Arrange

            // Act
            var teams = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Empty(teams);
        }

        [Fact]
        public void GetTeamsThatUserIn_UserIsInATeam_ReturnsEmptyList()
        {
            // Arrange

            Team team = new Team() { ID = 1, Name = "team" };
            User user = new User() { ID = 1, Name = "userName" };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = 1, TeamID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
                cnn.Insert(user);
                cnn.Insert(userIDToTeamID);
            }

            // Act
            var actualTeams = userDetailsPageService.GetTeamsThatUserIn();

            // Assert
            Assert.Contains(team, actualTeams);
        }
    }
}
