using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.UserServices
{
    public abstract class UserPageServiceTests : IntegrationTests
    {
        readonly UserPageService userPageService;
        public UserPageServiceTests()
        {
            userPageService = new UserPageService(databaseController);
        }

        [Fact]
        public void GetUsers_NoUserIsInDB_ReturnsEmptyList()
        {
            // Act
            var users = userPageService.GetUsers();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public void GetUsers_UserIsInDB_ReturnsUser()
        {
            // Arrange
            User user = new User() { Name = "user", Surname = "surname" };

            using (IDbConnection cnn = CreateConnection(connString))
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
            User user = new User() { Name = "user", Surname = "surname" };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }

        [Fact]
        public void DeleteUser_UserIsInDBNotInATeam_DeletesUser()
        {
            // Arrange
            User user = new User() { Name = "user", Surname = "surname" };

            using (IDbConnection cnn = CreateConnection(connString))
            {
                cnn.Insert(user);
            }

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            List<User> users;
            using (IDbConnection cnn = CreateConnection(connString))
            {
                users = cnn.GetAll<User>().ToList();
            }

            Assert.Empty(users);
        }

        [Fact]
        public void DeleteUser_UserIsInDBInATeam_DeletesUser()
        {
            // Arrange

            User user = new User() { ID = 1, Name = "user", Surname = "surname" };
            Team team = new Team() { ID = 1, Name = "team" };
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { ID = 1, TeamID = team.ID, UserID = user.ID };

            using (IDbConnection cnn = CreateConnection(connString))
            {
                cnn.Insert(user);
                cnn.Insert(team);
                cnn.Insert(userIDToTeamID);
            }

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            List<User> users;
            using (IDbConnection cnn = CreateConnection(connString))
            {
                users = cnn.GetAll<User>().ToList();
            }

            Assert.Empty(users);
        }
    }
}
