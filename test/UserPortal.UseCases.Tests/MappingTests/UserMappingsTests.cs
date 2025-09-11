using System;
using Bogus;
using FluentAssertions;
using UserPortal.Core.Constants;
using UserPortal.Core.Entities;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Mapping;

namespace UserPortal.UseCases.Tests.MappingTests;

public class UserMappingsTests
{
    private readonly Faker<User> _userFaker;
    private User _emptyUser;

    public UserMappingsTests()
    {
        _emptyUser = new User();
        _userFaker = new Faker<User>()
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.AvatarId, f => f.Random.Guid());
    }

    [Fact]
    public void ToProfileDto_CanMap_ToUserProfileDto()
    {
        // Arrange
        var user = _userFaker.Generate();

        // Act
        var result = user.ToProfileDto();

        // Assert
        result.Should().BeOfType<UserProfileDto>(); // ensure type
        result.UserName.Should().Be(user.UserName);
        result.FirstName.Should().Be(user.FirstName);
        result.LastName.Should().Be(user.LastName);
        result.Email.Should().Be(user.Email);
        result.AvatarId.Should().Be(user.AvatarId);
    }

    [Fact]
    public void ToProfileDto_Should_Return_Defaults_When_User_Is_Null()
    {
        // Act
        var result = _emptyUser.ToProfileDto();

        // Assert
        result.Should().BeOfType<UserProfileDto>(); // ensure type
        result.UserName.Should().Be(DefaultConstants.User.DefaultUserName);
        result.FirstName.Should().Be(DefaultConstants.User.DefaultFirstName);
        result.LastName.Should().Be(string.Empty);
        result.Email.Should().Be($"{DefaultConstants.User.DefaultUserName}@noemail.com");
        result.AvatarId.Should().Be(Guid.Empty);
    }

    [Fact]
    public void ToUserListDto_CanMapUserTo_UserListDto()
    {
        // Arrange
        var user = _userFaker.Generate();

        // Act
        var result = user.ToUserListDto();

        // Assert
        result.Should().BeOfType<UserListDto>(); // ensure type
        result.UserName.Should().Be(user.UserName);
        result.AvatarId.Should().Be(user.AvatarId);
    }

    [Fact]
    public void ToUserListDto_Should_Return_Defaults_When_User_Is_Null()
    {
        // Act
        var result = _emptyUser.ToUserListDto();

        // Assert
        result.Should().BeOfType<UserListDto>(); // ensure type
        result.UserName.Should().Be(DefaultConstants.User.DefaultUserName);
        result.AvatarId.Should().Be(Guid.Empty);
    }
}
