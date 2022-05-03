using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class TeamDetailsPageServiceTests
    {
        [Fact]
        public void GetUsersInTeam_TeamHasUsers_ReturnsUsers()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToAdd = new Team() { Name = "teamToDelete", ID = 1 };
            User expectedUser = new User() { Name = "User", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToAdd);
                cnn.Insert(expectedUser);
                cnn.Insert(userIDToTeamID);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam(teamToAdd);
            Assert.Contains(actualUsers, u => u.Name == expectedUser.Name);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToAdd = new Team() { Name = "teamToDelete", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToAdd);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam(teamToAdd);
            Assert.Empty(actualUsers);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void GetUsersInTeam_NoTeamExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team team = new Team() { Name = "teamToDelete", ID = 1 };

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam(team);
            Assert.Empty(actualUsers);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void GetUsersInTeam_NoSuchTeamExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamNotInDB = new Team() { Name = "team1", ID = 1 };
            Team teamInDB = new Team() { Name = "team2", ID = 2 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamInDB);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam(teamNotInDB);
            Assert.Empty(actualUsers);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void GetUsersInTeam_NoSuchTeamExistsInDBOtherTeamsWithUsersExist_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamNotInDB = new Team() { Name = "team1", ID = 1 };
            Team teamInDB = new Team() { Name = "team2", ID = 2 };
            User userInTeamInDB = new User() { ID = 1, Name = "user" };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 2, UserID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamInDB);
                cnn.Insert(userInTeamInDB);
                cnn.Insert(userIDToTeamID);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam(teamNotInDB);
            Assert.Empty(actualUsers);

            helperMethods.DeleteDB(dbPath);
        }
    }
}
