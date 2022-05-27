using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.DatabaseControllers;
using TeamManager.Service.Wizard.Models;
using Xunit;

namespace TeamManager.Service.UnitTest.Wizard
{
    public class LoginPageServiceTests
    {
        readonly Manager validManager;
        readonly LoginPageService loginPageService;
        public LoginPageServiceTests()
        {
            string userName = "CorrectName";
            string password = "CorrectPassword";

            validManager = new Manager()
            {
                UserName = userName,
                Password = password
            };

            var connection = new Mock<WizardDatabaseController>("connectionString");
            connection.Setup(x => x.GetManagers()).Returns(new List<Manager>() { validManager });

            loginPageService = new LoginPageService(connection.Object);
        }

        [Fact]
        public void GetManager_ValidUserNameValidPassword_ReturnsManager()
        {
            // Act 
            Manager actualManager = loginPageService.GetManager(validManager);

            // Assert
            Assert.Equal(validManager, actualManager);
        }

        [Fact]
        public void GetManager_ValidUserNameInvalidPassword_CantReturnManager()
        {
            // Arrange
            Manager manager = new Manager()
            {
                UserName = "CorrectName",
                Password = "InvalidPassword"
            };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => loginPageService.GetManager(manager));
        }

        [Fact]
        public void GetManager_InvalidUserNameValidPassword_CantReturnManager()
        {
            // Arrange
            string invalidUserName = "InvalidName";
            string password = "CorrectPassword";

            Manager invalidManager = new Manager()
            {
                UserName = invalidUserName,
                Password = password
            };

            // Act && Assert
            Assert.Throws<ArgumentException>(() => loginPageService.GetManager(invalidManager));
        }
    }
}