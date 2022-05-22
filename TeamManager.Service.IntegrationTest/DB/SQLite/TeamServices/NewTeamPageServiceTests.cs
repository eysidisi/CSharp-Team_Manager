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
    public class NewTeamPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void SaveTeam_NoTeamExistsInDB_AddsTeam()
        {
            // Arrange
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection);

            Team team = new Team() { Name = "Team1", ID = 1 };

            // Act
            newTeamPageService.SaveTeam(team);

            // Assert
            List<Team> actualTeams;

            using (var cnn = new SQLiteConnection(connString))
            {
                actualTeams = cnn.GetAll<Team>().ToList();
            }

            Assert.Contains(team, actualTeams);
        }

        [Fact]
        public void SaveTeam_TeamWithSameNameExistsInDB_ThrowsException()
        {
            // Arrange
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection);
            Team team = new Team() { Name = "Team1" };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
            }

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(team));
        }

        [Fact]
        public void SaveTeam_InvalidTeam_ThrowsException()
        {
            // Arrange
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection);
            Team team = new Team() { };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newTeamPageService.SaveTeam(team));
        }
    }
}
