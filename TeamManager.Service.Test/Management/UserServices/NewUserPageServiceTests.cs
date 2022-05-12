using Moq;
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
        Mock<IManagementDatabaseConnection> connection;

        NewUserPageService newUserPageService;

        public NewUserPageServiceTests()
        {
            connection = new Mock<IManagementDatabaseConnection>();
            newUserPageService = new NewUserPageService(connection.Object);
        }

        [Fact]
        public void AddUser_ValidUser_AddsUser()
        {
            // Arrange
            User expectedUserToSave = new User()
            {
                Name = "name",
                CreationDate = "123",
                PhoneNumber = "1234",
                Surname = "surname",
                Title = "title"
            };

            // Act 
            newUserPageService.SaveNewUser(expectedUserToSave);

            // Assert
            connection.Verify(c => c.SaveUser(It.Is<User>(actualUserToSave => actualUserToSave.Equals(expectedUserToSave))));
        }

        [Fact]
        public void AddUser_InvalidUser_ThrowsException()
        {
            // Arrange
            User user = new User();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}
