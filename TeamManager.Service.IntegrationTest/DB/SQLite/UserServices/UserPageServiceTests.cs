﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class UserPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void GetUsers_NoUserIsInDB_ReturnsEmptyList()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            // Act
            var users = userPageService.GetUsers();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GetUsers_UserIsInDB_ReturnsUser()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() { Name = "user", Surname = "surname" };

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
            }

            // Act
            var actualUsers = userPageService.GetUsers();

            // Assert
            Assert.Contains(user, actualUsers);
        }

        [Fact]
        public void DeleteUser_NoUserIsInDB_ThrowsException()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() { Name = "user", Surname = "surname" };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }

        [Fact]
        public void DeleteUser_UserIsInDBNotInATeam_DeletesUser()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() { Name = "user", Surname = "surname" };

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
            }

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            List<User> users;
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                users = cnn.GetAll<User>().ToList();
            }

            Assert.Empty(users);
        }

        [Fact]
        public void DeleteUser_UserIsInDBInATeam_DeletesUser()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() {ID=1, Name = "user", Surname = "surname" };
            Team team = new Team() { ID = 1, Name = "team" };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = team.ID, UserID = user.ID };

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
                cnn.Insert(team);
                cnn.Insert(userIDToTeamID);
            }

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            List<User> users;
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                users = cnn.GetAll<User>().ToList();
            }

            Assert.Empty(users);
        }
    }
}
