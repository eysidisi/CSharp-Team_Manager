using Dapper.Contrib.Extensions;
using System.Data.SQLite;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.TeamServices
{
    public abstract class TeamDetailsPageServiceTests : IntegrationTests
    {
        readonly Team temToGetDetails;
        readonly TeamDetailsPageService teamDetailsPageService;


        public TeamDetailsPageServiceTests()
        {
            temToGetDetails = new Team() { Name = "team", ID = 1 };
            teamDetailsPageService = new TeamDetailsPageService(databaseController, temToGetDetails);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasUsers_ReturnsUsers()
        {
            // Arrange
            User expectedUser = new User() { Name = "user", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(temToGetDetails);
                cnn.Insert(expectedUser);
                cnn.Insert(userIDToTeamID);
            }

            ManagerSQLiteDatabaseController connection = new ManagerSQLiteDatabaseController(connString);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam();
            Assert.Contains(actualUsers, u => u.Name == expectedUser.Name);
        }

        [Fact]
        public void GetUsersInTeam_TeamHasNoUsers_ReturnsEmptyList()
        {
            // Arrange
            Team temToGetDetails = new Team() { Name = "team", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(temToGetDetails);
            }

            ManagerSQLiteDatabaseController connection = new ManagerSQLiteDatabaseController(connString);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_NoTeamExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            Team team = new Team() { Name = "team", ID = 1 };
            ManagerSQLiteDatabaseController connection = new ManagerSQLiteDatabaseController(connString);

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_NoSuchTeamExistsInDB_ReturnsEmptyList()
        {
            // Arrange
            Team teamNotInDB = new Team() { Name = "team1", ID = 1 };
            Team teamInDB = new Team() { Name = "team2", ID = 2 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamInDB);
            }

            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void GetUsersInTeam_NoSuchTeamExistsInDBOtherTeamsWithUsersExist_ReturnsEmptyList()
        {
            // Arrange
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


            // Act 
            var actualUsers = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(actualUsers);
        }
    }
}
