using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using Xunit;

namespace TeamManager.Service.IntegrationTest.DB.UserServices
{
    public abstract class NewUserPageServiceTests : IntegrationTests
    {
        readonly NewUserPageService newUserPageService;
        public NewUserPageServiceTests()
        {
            newUserPageService = new NewUserPageService(databaseController);
        }

        [Fact]
        public void SaveNewUser_EmptyDB_SavesNewUser()
        {
            // Arrange
            User user = new User() { Name = "userName", Surname = "userSurname", ID = 1 };

            // Act
            newUserPageService.SaveNewUser(user);

            // Assert
            List<User> actualUsers;

            using (var cnn = CreateConnection(connString))
            {
                actualUsers = cnn.GetAll<User>().ToList();
            }

            Assert.Equal(new List<User>() { user }, actualUsers);
        }

        [Fact]
        public void SaveNewUser_EmptyDBInvalidUser_ThrowsException()
        {
            // Arrange
            User user = new User() { Surname = "userSurname", ID = 1 };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}