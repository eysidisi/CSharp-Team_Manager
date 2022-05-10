﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management
{
    public class NewUserPageServiceTests
    {
        [Fact]
        public void AddUser_ValidUser_AddsUser()
        {
            // Arrange
            var connection = new Mock<IManagementDatabaseConnection>();

            var newUserPageService = new NewUserPageService(connection.Object);
            User user = new User()
            {
                Name = "name",
                CreationDate = "123",
                PhoneNumber = "1234",
                Surname = "surname",
                Title = "title"
            };

            // Act && Assert
            newUserPageService.SaveNewUser(user);
        }
        [Fact]
        public void AddUser_InvalidUser_CantAddUser()
        {
            // Arrange
            var connection = new Mock<IManagementDatabaseConnection>();

            var newUserPageService = new NewUserPageService(connection.Object);
            User user = new User();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}
