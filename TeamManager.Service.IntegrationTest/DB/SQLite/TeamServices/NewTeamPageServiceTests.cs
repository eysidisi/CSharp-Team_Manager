using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.TeamServices
{
    public class NewTeamPageServiceTests:SQLiteIntegrationTestsBase
    {
        [Fact]
        public void SaveTeam_NoTeamExistsInDB_AddsTeam()
        {
            // Arrange
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection);

            Team team = new Team() { Name = "Team1" };

            // Act
            newTeamPageService.SaveTeam(team);

            // Assert
            List<Team> teams;

            using (var cnn = new SQLiteConnection(connString))
            {
                teams = cnn.GetAll<Team>().ToList();
            }

            Assert.Contains(teams, t => t.Name == team.Name);
        }
    }
}
