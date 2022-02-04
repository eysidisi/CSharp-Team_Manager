using Moq;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.LoginPage;
using Xunit;

namespace TeamManager.Service.Test
{
    public class UserInfoTest
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

            LoginPageChecks userInfo = new LoginPageChecks(connection.Object);


            // Act
            bool result = userInfo.CheckIfUserExists(userName, password);

            // Assert
            Assert.True(result);
        }
    }
}