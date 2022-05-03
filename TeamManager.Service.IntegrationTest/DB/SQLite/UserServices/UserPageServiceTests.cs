using Dapper.Contrib.Extensions;
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

            User user = new User() { Name = "user1", Surname = "surname1" };

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
            }

            // Act
            var users = userPageService.GetUsers();

            // Assert
            Assert.Contains(users, u => u.Name == user.Name && u.Surname == user.Surname);
        }

        [Fact]
        public void DeleteUser_NoUserIsInDB_ReturnsUser()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() { Name = "user1", Surname = "surname1" };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }

        [Fact]
        public void DeleteUser_UserIsInDB_ReturnsUser()
        {
            // Arrange
            UserPageService userPageService = new UserPageService(connection);

            User user = new User() { Name = "user1", Surname = "surname1" };

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
    }
}
