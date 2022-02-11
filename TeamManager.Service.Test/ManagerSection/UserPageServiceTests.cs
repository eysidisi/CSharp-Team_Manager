using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.ManagerSection;
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
            var connection = new Mock<IDatabaseConnection>();

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
            var connection = new Mock<IDatabaseConnection>();

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act
            User user = new User();

            // Assert
            userPageService.DeleteUser(user);
        }

    }
}
