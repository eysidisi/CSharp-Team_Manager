using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class NewUserPageServiceTests
    {
        [Fact]
        public void AddUser_ValidUser_AddsUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

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
            var connection = new Mock<IManagerDatabaseConnection>();

            var newUserPageService = new NewUserPageService(connection.Object);
            User user = new User();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}
