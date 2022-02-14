using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Models;
using TeamManager.Service.WizardSection;
using TeamManager.Service.WizardSection.Database;
using Xunit;

namespace TeamManager.Service.Test.WizardSection
{
    public class LoginPageServiceTests
    {
        [Fact]
        public void CheckManagerInfo_ValidUserNameValidPassword()
        {
            // Arrange
            string userName = "CorrectName";
            string password = "CorrectPassword";

            Manager manager = new Manager()
            {
                UserName = userName,
                Password = password
            };

            var connection = new Mock<IWizardDatabaseConnection>();
            connection.Setup(x => x.GetManagers()).Returns(new List<Manager>() { manager });

            LoginPageService loginPageService = new LoginPageService(connection.Object);

            // Act && Assert
            var actualManager = loginPageService.GetManager(manager);
        }
        [Fact]
        public void CheckManagerInfo_ValidUserNameInvalidPassword()
        {
            // Arrange
            string userName = "CorrectName";
            string password = "InvalidPassword";

            Manager manager = new Manager()
            {
                UserName = userName,
                Password = password
            };


            Manager managerInDB = new Manager()
            {
                UserName = userName,
                Password = "CorrectPassword"
            };

            var connection = new Mock<IWizardDatabaseConnection>();
            connection.Setup(x => x.GetManagers()).Returns(new List<Manager>() { managerInDB });

            LoginPageService loginPageService = new LoginPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => loginPageService.GetManager(manager));
        }
        [Fact]
        public void CheckManagerInfo_InvalidUserNameValidPassword()
        {
            // Arrange
            string invalidUserName = "InvalidName";
            string validUserName = "ValidName";
            string password = "CorrectPassword";

            Manager invalidManager = new Manager()
            {
                UserName = invalidUserName,
                Password = password
            };

            Manager validManager = new Manager()
            {
                UserName = validUserName,
                Password = password
            };

            var connection = new Mock<IWizardDatabaseConnection>();
            connection.Setup(x => x.GetManagers()).Returns(new List<Manager>() { validManager });

            LoginPageService loginPageService = new LoginPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => loginPageService.GetManager(invalidManager));
        }
    }
}