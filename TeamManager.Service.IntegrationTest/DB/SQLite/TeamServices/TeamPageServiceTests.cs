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
    public class TeamPageServiceTests
    {
        [Fact]
        public void GetAllTeams_DBHasNoTeams_ReturnsEmptyList()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            var teams = teamPageService.GetAllTeams();

            // Assert
            Assert.Empty(teams);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void GetAllTeams_DBHasTeams_ReturnsTeams()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            List<Team> expectedTeams = new List<Team>()
            {
                new Team(){Name="team1"},
                new Team(){Name="team2"}
            };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(expectedTeams);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            var actualTeams = teamPageService.GetAllTeams();

            // Assert
            foreach (var expectedTeam in expectedTeams)
            {
                Assert.Contains(actualTeams, a => a.Name == expectedTeam.Name);
            }

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void DeleteTeam_DBHasTheTeam_DeletesTeam()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 0 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToDelete);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            teamPageService.DeleteTeam(teamToDelete);

            // Assert
            List<Team> teamsLeftInDB;
            using (var cnn = new SQLiteConnection(connString))
            {
                teamsLeftInDB = cnn.GetAll<Team>().ToList();
            }

            Assert.DoesNotContain(teamsLeftInDB, t => t.Name == teamToDelete.Name);

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void DeleteTeam_DBHasNoTeams_ThrowsException()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 0 };

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void DeleteTeam_DBDoesNotHaveThatTeam_ThrowsException()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToAdd = new Team() { Name = "teamToAdd", ID = 1 };
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 0 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToAdd);
            }


            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));

            helperMethods.DeleteDB(dbPath);
        }

        [Fact]
        public void DeleteTeam_TeamHasUsers_ThrowsException()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToDelete);
                cnn.Insert(userIDToTeamID);
            }

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));

            helperMethods.DeleteDB(dbPath);
        }
    }
}
