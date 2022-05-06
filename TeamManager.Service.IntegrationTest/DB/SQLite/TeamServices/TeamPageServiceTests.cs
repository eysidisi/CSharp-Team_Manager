﻿using Dapper.Contrib.Extensions;
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
    public class TeamPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void GetAllTeams_DBHasNoTeams_ReturnsEmptyList()
        {
            // Arrange
            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            var teams = teamPageService.GetAllTeams();

            // Assert
            Assert.Empty(teams);
        }

        [Fact]
        public void GetAllTeams_DBHasTeams_ReturnsTeams()
        {
            // Arrange
            List<Team> expectedTeams = new List<Team>()
            {
                new Team(){Name="team1",ID=1},
                new Team(){Name="team2",ID=2}
            };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(expectedTeams);
            }

            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            var actualTeams = teamPageService.GetAllTeams();

            // Assert
            Assert.Equal(expectedTeams, actualTeams);
        }

        [Fact]
        public void DeleteTeam_DBHasTheTeam_DeletesTeam()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToDelete);
            }

            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act
            teamPageService.DeleteTeam(teamToDelete);

            // Assert
            List<Team> teamsLeftInDB;
            using (var cnn = new SQLiteConnection(connString))
            {
                teamsLeftInDB = cnn.GetAll<Team>().ToList();
            }

            Assert.DoesNotContain(teamToDelete,teamsLeftInDB);
        }

        [Fact]
        public void DeleteTeam_DBHasNoTeams_ThrowsException()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };

            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }

        [Fact]
        public void DeleteTeam_DBDoesNotHaveThatTeam_ThrowsException()
        {
            // Arrange
            Team teamToAdd = new Team() { Name = "teamToAdd", ID = 1 };
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 2 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToAdd);
            }


            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }

        [Fact]
        public void DeleteTeam_TeamHasUsers_ThrowsException()
        {
            // Arrange
            Team teamToDelete = new Team() { Name = "teamToDelete", ID = 1 };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = 1, UserID = 1 };

            using (var cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(teamToDelete);
                cnn.Insert(userIDToTeamID);
            }

            ManagementSQLiteConnetion connection = new ManagementSQLiteConnetion(connString);
            TeamPageService teamPageService = new TeamPageService(connection);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(teamToDelete));
        }
    }
}
