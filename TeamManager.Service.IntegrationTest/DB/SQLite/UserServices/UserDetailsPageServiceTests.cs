using Dapper.Contrib.Extensions;
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
    public class UserDetailsPageServiceTests
    {
        [Fact]
        public void GetTeamsThatUserIn_EmptyDB_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            UserDetailsPageService userDetailsPageService = new UserDetailsPageService(connection);

            User user = new User();

            // Act
            var teams = userDetailsPageService.GetTeamsThatUserIn(user);

            // Assert
            Assert.Empty(teams);
        }

        [Fact]
        public void GetTeamsThatUserIn_UserIsInATeam_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            UserDetailsPageService userDetailsPageService = new UserDetailsPageService(connection);

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
            var teams = userDetailsPageService.GetTeamsThatUserIn(user);

            // Assert
            Assert.Single(teams);
            Assert.Contains(teams, t => t.ID == team.ID && t.Name == team.Name);
        }
    }
}
