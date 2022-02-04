using Moq;
using TeamManager.Service.Wizard;
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
                Is<User>(user => user.userName == userName && user.password == password))).Returns(true);

            LoginPageService userInfo = new LoginPageService(connection.Object);

            // Act
            bool result = userInfo.CheckIfUserExists(userName, password);

            // Assert
            Assert.True(result);
        }
    }
}