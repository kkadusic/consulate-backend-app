﻿using Moq;
using NSI.BusinessLogic.Implementations;
using NSI.Common.Enumerations;
using NSI.Repository.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace NSI.Tests.ManipulationTests
{
    public class UserPermissionManipulationTests
    {
        [Fact]
        public void GetUsersAndTheirPermissions_ReturnsUsersAndTheirPermissions()
        {
            // Arrange
            var usersPermMock = new Mock<IUserPermissionRepository>();
            usersPermMock.Setup(MockItem => MockItem.GetUsersAndPermissionsAsync())
                .ReturnsAsync(() => { return null; });
            var userPermissionManipulation = new UserPermissionManipulation(usersPermMock.Object);

            // Act
            var result = userPermissionManipulation.GetUsersAndTheirPermissionsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPermssionsForUser_CorrectEmail_ReturnsPermissions()
        {
            // Arrange
            var usersPermMock = new Mock<IUserPermissionRepository>();
            usersPermMock.Setup(MockItem => MockItem.GetUserPermissionsAsync("alakovic1@etf.unsa.ba")).ReturnsAsync(
                () =>
                {
                    List<PermissionEnum> users = new List<PermissionEnum>();
                    users.Add(PermissionEnum.None);
                    return users;
                });
            var userPermissionManipulation = new UserPermissionManipulation(usersPermMock.Object);

            // Act
            var result = userPermissionManipulation.GetPermissionsForUserAsync("alakovic1@etf.unsa.ba");

            // Assert
            Assert.Single(result.Result);
        }

        [Fact]
        public void GetPermssionsForUser_CorrectEmail_ReturnsNull()
        {
            // Arrange
            var usersPermMock = new Mock<IUserPermissionRepository>();
            usersPermMock.Setup(MockItem => MockItem.GetUserPermissionsAsync("alakovic1@etf.unsa.ba"))
                .ReturnsAsync(() => { return null; });
            var userPermissionManipulation = new UserPermissionManipulation(usersPermMock.Object);

            // Act
            var result = userPermissionManipulation.GetPermissionsForUserAsync("alakovic1@etf.unsa.ba");

            // Assert
            Assert.NotNull(result);
        }
    }
}
