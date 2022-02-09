using Moq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Database;
using TeamManager.Service.Wizard.LoginPage;
using Xunit;
using TeamManager.Service.Models;

namespace TeamManager.Service.Test.Wizard.LoginPage
{
    public class LoginPageServiceTests
    {
        [Fact]
        public void CheckUserInfo_ReturnsTrue()
        {
            // Arrange
            string userName = "CorrectName";
            string password = "CorrectPassword";

            var connection = new Mock<IDatabaseConnection>();
            connection.Setup(x => x.CheckIfManagerExists(It.
                Is<Manager>(manager => manager.UserName == userName && manager.Password == password))).Returns(true);

            LoginPageService managerInfo = new LoginPageService(connection.Object);

            // Act
            bool result = managerInfo.CheckIfUserExists(userName, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetUser_ReturnsTrue()
        {
            // Arrange
            string userName = "CorrectName";
            string password = "CorrectPassword";

            Manager expectedUser = new Manager(userName, password);

            var connection = new Mock<IDatabaseConnection>();
            connection.Setup(x => x.GetManager(It.
                Is<string>(u => u == userName))).Returns(expectedUser);

            LoginPageService managerInfo = new LoginPageService(connection.Object);

            // Act
            var actualUser = managerInfo.GetManager(userName);

            // Assert
            Assert.Equal(expectedUser, actualUser);
        }

    }
}