﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.ManagerSection.TeamServices;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection.TeamServices
{
    public class EditTeamPageServiceTests
    {
        [Fact]
        public void AddUserToTheTeam_NoEntryInDB_AddsUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns<List<UserIDToTeamID>>(null);
            EditTeamPageService editTeamPage = new EditTeamPageService(connection.Object);

            Team teamToAdd = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            // Act
            editTeamPage.AddUserToTheTeam(userToAdd, teamToAdd);

            // Assert
            connection.Verify(c => c.GetAllUserIDToTeamID());
        }

        [Fact]
        public void AddUserToTheTeam_HasDifferentEntryInDB_AddsUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();
            EditTeamPageService editTeamPage = new EditTeamPageService(connection.Object);

            Team teamToAdd = new Team() { ID = 1, Name = "team1", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            Team team2 = new Team() { ID = 2, Name = "team2", CreationDate = "1234" };
            User user2 = new User() { ID = 2, Name = "user2", CreationDate = "1234" };
            User user3 = new User() { ID = 3, Name = "user3", CreationDate = "1234" };

            UserIDToTeamID userInTeam1 = new UserIDToTeamID() { ID = 1, UserID = user2.ID, TeamID = teamToAdd.ID };
            UserIDToTeamID userInTeam2 = new UserIDToTeamID() { ID = 2, UserID = user3.ID, TeamID = team2.ID };

            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userInTeam1, userInTeam2 };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);

            // Act
            editTeamPage.AddUserToTheTeam(userToAdd, teamToAdd);

            // Assert
            connection.Verify(c => c.GetAllUserIDToTeamID());
        }

        [Fact]
        public void AddUserToTheTeam_UserIsAlreadyInTheTeam_CantAddUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();
            EditTeamPageService editTeamPage = new EditTeamPageService(connection.Object);

            Team teamToAdd = new Team() { ID = 1, Name = "team", CreationDate = "1234" };
            User userToAdd = new User() { ID = 1, Name = "user", CreationDate = "1234" };

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, UserID = userToAdd.ID, TeamID = teamToAdd.ID };
            List<UserIDToTeamID> userIDsToTeamIDs = new List<UserIDToTeamID>() { userIDToTeamID };

            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(userIDsToTeamIDs);

            // Act
            Assert.Throws<ArgumentException>(() => editTeamPage.AddUserToTheTeam(userToAdd, teamToAdd));

            // Assert
        }
    }
}
