using Moq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service.Wizard.LoginPage;
using Xunit;

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
            connection.Setup(x => x.CheckIfUserExists(It.
                Is<User>(user => user.UserName == userName && user.Password == password))).Returns(true);

            LoginPageService userInfo = new LoginPageService(connection.Object);

            // Act
            bool result = userInfo.CheckIfUserExists(userName, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetUser_ReturnsTrue()
        {
            // Arrange
            string userName = "CorrectName";
            string password = "CorrectPassword";

            User expectedUser = new User(userName, password);

            var connection = new Mock<IDatabaseConnection>();
            connection.Setup(x => x.GetUser(It.
                Is<string>(u => u == userName))).Returns(expectedUser);

            LoginPageService userInfo = new LoginPageService(connection.Object);

            // Act
            var actualUser = userInfo.GetUser(userName);

            // Assert
            Assert.Equal(expectedUser, actualUser);
        }

    }
}