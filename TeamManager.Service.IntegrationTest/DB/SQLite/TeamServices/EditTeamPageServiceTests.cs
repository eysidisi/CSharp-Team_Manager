using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class EditTeamPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void AddUserToTheTeam_UserAndTeamExists_AddsUserToTheTeam()
        {
            // Arrange
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };
            User user = new User() { Name = "User1", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
                cnn.Insert(user);
            }
            
            // Act
            editTeamPageService.AddUserToTheTeam(user, team);

            // Assert
            List<UserIDToTeamID> userIDToTeamIDs;

            using (var cnn = new SQLiteConnection(connString))
            {
                userIDToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
            }

            Assert.Contains(userIDToTeamIDs, u => u.UserID == team.ID && u.TeamID == team.ID);
        }
    }
}
