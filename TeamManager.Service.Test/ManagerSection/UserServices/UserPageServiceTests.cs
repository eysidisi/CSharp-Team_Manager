using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.ManagerSection
{
    public class UserPageServiceTests
    {
        [Fact]
        public void GetUsers_UserExistsInDB_GetsUsers()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();
            UserPageService userPageService = new UserPageService(connection.Object);

            User user = new User();
            var expectedUsers = new List<User>() { user };

            connection.Setup(c => c.GetAllUsers()).Returns(expectedUsers);

            // Act
            var actualUsers = userPageService.GetUsers();

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void GetUsers_NoUserExistsInDB_GetsEmptyList()
        {
            // Arrange
            var connection = new Mock<IManagerDatabaseConnection>();
            UserPageService userPageService = new UserPageService(connection.Object);

            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());

            // Act
            var actualUsers = userPageService.GetUsers();

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void DeleteUser_UserExistsInDB_DeletesUser()
        {
            // Arrange
            User user = new User();
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(true);

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act && Assert
            userPageService.DeleteUser(user);
        }

        [Fact]
        public void DeleteUser_UserDoesntExistInDB_CantDeleteUser()
        {
            // Arrange
            User user = new User();
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(false);

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }
    }
}
