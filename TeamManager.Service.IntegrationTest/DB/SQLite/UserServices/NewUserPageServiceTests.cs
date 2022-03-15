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
    public class NewUserPageServiceTests
    {
        [Fact]
        public void SaveNewUser_EmptyDB_SavesNewUser()
        {
            // Arrange
            HelperMethods helperMethods = new HelperMethods();
            var dbPath = helperMethods.CreateEmptyTestDB_ReturnFilePath();
            string connString = $"Data Source={dbPath}";

            ManagerSQLiteConnetion connection = new ManagerSQLiteConnetion(connString);
            NewUserPageService newUserPageService = new NewUserPageService(connection);

            User user = new User() { Name = "userName", Surname = "userSurname" };

            // Act
            newUserPageService.SaveNewUser(user);

            // Assert
            List<User> users;

            using (var cnn = new SQLiteConnection(connString))
            {
                users = cnn.GetAll<User>().ToList();
            }

            Assert.Contains(users, u => u.Name == user.Name && u.Surname == user.Surname);

            helperMethods.DeleteDB(dbPath);
        }
    }
}