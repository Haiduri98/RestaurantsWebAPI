﻿using FluentAssertions;
using JetBrains.Annotations;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Tests.Users;

[TestSubject(typeof(CurrentUser))]
public class CurrentUserTest
{
    // TestMethod_Scenario_ExpectResult

    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRoleTest_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "Test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var isInRole = currentUser.IsInRole(roleName);

        // assert
        isInRole.Should().BeTrue();
    }


    [Fact]
    public void IsInRoleTest_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "Test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // assert
        isInRole.Should().BeFalse();
    }


    [Fact]
    public void IsInRoleTest_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "Test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert
        isInRole.Should().BeFalse();
    }

}