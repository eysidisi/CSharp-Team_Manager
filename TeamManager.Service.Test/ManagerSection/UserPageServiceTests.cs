using Moq;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class UserPageServiceTests
    {
        [Fact]
        public void AddUser_AddsUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act
            User user = new User();

            // Assert
            userPageService.AddUser(user);
        }

        [Fact]
        public void DeleteUser_DeletesUser()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act
            User user = new User();

            // Assert
            userPageService.DeleteUser(user);
        }

    }
}
