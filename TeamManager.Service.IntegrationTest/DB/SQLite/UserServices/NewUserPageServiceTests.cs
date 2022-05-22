using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class NewUserPageServiceTests : SQLiteIntegrationTestsBase
    {
        [Fact]
        public void SaveNewUser_EmptyDB_SavesNewUser()
        {
            // Arrange
            NewUserPageService newUserPageService = new NewUserPageService(databaseManager);

            User user = new User() { Name = "userName", Surname = "userSurname", ID = 1 };

            // Act
            newUserPageService.SaveNewUser(user);

            // Assert
            List<User> actualUsers;

            using (var cnn = new SQLiteConnection(connString))
            {
                actualUsers = cnn.GetAll<User>().ToList();
            }

            Assert.Contains(user, actualUsers);
        }

        [Fact]
        public void SaveNewUser_EmptyDBInvalidUser_ThrowsException()
        {
            // Arrange
            NewUserPageService newUserPageService = new NewUserPageService(databaseManager);
            User user = new User() { Surname = "userSurname", ID = 1 };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}