using Xunit;
using TeamManager.Service.Test;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management;
using TeamManager.Service.Models;
using System.Data.SQLite;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TeamManager.Service.IntegrationTest.DB.SQLite.UserServices
{
    public class NewUserPageServiceTests:SQLiteIntegrationTestsBase
    {
        [Fact]
        public void SaveNewUser_EmptyDB_SavesNewUser()
        {
            // Arrange
            NewUserPageService newUserPageService = new NewUserPageService(connection);

            User user = new User() { Name = "userName", Surname = "userSurname" ,ID=1};

            // Act
            newUserPageService.SaveNewUser(user);

            // Assert
            List<User> actualUsers;

            using (var cnn = new SQLiteConnection(connString))
            {
                actualUsers = cnn.GetAll<User>().ToList();
            }

            Assert.Contains(user,actualUsers);
        }
    }
}