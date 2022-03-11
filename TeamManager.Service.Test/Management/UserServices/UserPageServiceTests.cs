﻿using Moq;
using System;
using System.Collections.Generic;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using Xunit;

namespace TeamManager.Service.Test.Management
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
        public void DeleteUser_UserExistsInDBUserIsInNoTeam_DeletesUser()
        {
            // Arrange
            User user = new User();
            var connection = new Mock<IManagerDatabaseConnection>();
            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>());

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act && Assert
            userPageService.DeleteUser(user);
        }

        [Fact]
        public void DeleteUser_UserExistsInDBUserIsInTeam_DeletesUser()
        {
            // Arrange
            User user = new User()
            {
                ID = 1,
                Name = "user"
            };

            var userIDToTeamID = new UserIDToTeamID()
            {
                TeamID = 1,
                UserID = 1
            };


            var connection = new Mock<IManagerDatabaseConnection>();

            connection.Setup(c => c.DeleteUser(It.Is<User>(u => u == user))).Returns(true);
            connection.Setup(c => c.GetAllUserIDToTeamID()).Returns(new List<UserIDToTeamID>() { userIDToTeamID });

            UserPageService userPageService = new UserPageService(connection.Object);

            // Act 
            userPageService.DeleteUser(user);

            // Assert
            connection.Verify(c => c.DeleteUserIDToTeamID(userIDToTeamID));
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