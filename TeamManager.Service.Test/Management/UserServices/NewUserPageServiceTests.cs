using Moq;
using System;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class NewUserPageServiceTests : UserServicesTestsBase
    {
        readonly NewUserPageService newUserPageService;

        public NewUserPageServiceTests()
        {
            newUserPageService = new NewUserPageService(databaseController.Object);
        }

        [Fact]
        public void SaveNewUser_ValidUser_AddsUser()
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
            databaseController.Verify(c => c.SaveUser(It.Is<User>(actualUserToSave => actualUserToSave.Equals(expectedUserToSave))));
        }

        [Fact]
        public void SaveNewUser_InvalidUser_ThrowsException()
        {
            // Arrange
            User user = new User();

            // Act && Assert
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(user));
        }
    }
}
