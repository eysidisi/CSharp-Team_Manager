using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using Xunit;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class UserPageServiceTests : UserServicesTestsBase
    {
        readonly UserPageService userPageService;

        public UserPageServiceTests()
        {
            userPageService = new UserPageService(databaseController);
        }

        [Fact]
        public void GetUsers_UserExistsInDB_GetsUsers()
        {
            // Arrange
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
            connection.Setup(c => c.GetAllUsers()).Returns(new List<User>());

            // Act
            var actualUsers = userPageService.GetUsers();

            // Assert
            Assert.Empty(actualUsers);
        }

        [Fact]
        public void DeleteUser_UserExistsInDBUserIsInNoTeam_DeletesUser()
        {
            // Arrange
            User user = new User() { ID = 1, Name = "user" };
            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            connection.Verify(c => c.DeleteUser(It.Is<User>(actualDeletedUser => actualDeletedUser.Equals(user))));
        }

        [Fact]
        public void DeleteUser_UserExistsInDBUserIsInTeam_DeletesUser()
        {
            // Arrange
            User user = new User() { ID = 1, Name = "user" };
            var userIDToTeamID = new UserIDToTeamID() { TeamID = 1, UserID = 1 };

            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            connection.Verify(c => c.DeleteUserIDToTeamID(userIDToTeamID));
        }

        [Fact]
        public void DeleteUser_UserDoesntExistInDB_ThrowsException()
        {
            // Arrange
            User user = new User();
            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(false);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }
    }
}
