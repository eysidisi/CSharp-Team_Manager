using Moq;
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
            connection.Setup(x => x.GetManager(It.Is<string>(m => m == userName))).
                                                                Returns(manager);

            LoginPageService managerInfo = new LoginPageService(connection.Object);

            // Act
            bool result = managerInfo.CheckIfManagerExists(manager);

            // Assert
            Assert.True(result);
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
            connection.Setup(x => x.GetManager(userName)).Returns(managerInDB);

            LoginPageService managerInfo = new LoginPageService(connection.Object);

            // Act
            bool result = managerInfo.CheckIfManagerExists(manager);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void CheckManagerInfo_InvalidUserNameValidPassword()
        {
            // Arrange
            string userName = "InvalidName";
            string password = "CorrectPassword";

            Manager manager = new Manager()
            {
                UserName = userName,
                Password = password
            };

            var connection = new Mock<IWizardDatabaseConnection>();
            connection.Setup(x => x.GetManager(userName)).Returns<Manager>(null);

            LoginPageService managerInfo = new LoginPageService(connection.Object);

            // Act
            bool result = managerInfo.CheckIfManagerExists(manager);

            // Assert
            Assert.False(result);
        }
    }
}