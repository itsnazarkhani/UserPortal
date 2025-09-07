using System;
using FluentAssertions;
using UserPortal.Core.Entities;

namespace UserPortal.Core.Tests.EntitiesTests;

public class ApplicationUserTests
{
    [Fact]
    public void RedundentWhiteSpaces_GetsTrimed_WhenSettingFirstAndLastNames()
    {
        // Arrange & Act
        var whiteSpaceContainingFirstName = " Ali  ";
        var whiteSpaceContainingLastName = "  Ahmadi  ";
        var user = new ApplicationUser()
        {
            FirstName = whiteSpaceContainingFirstName,
            LastName = whiteSpaceContainingLastName
        };

        // Assert
        user.FirstName.Should().Be(whiteSpaceContainingFirstName.Trim());
        user.LastName.Should().Be(whiteSpaceContainingLastName.Trim());
    }

    [Theory]
    [InlineData("Ali", "Ahmadi")]
    [InlineData("Ali", " Ahmadi")]
    [InlineData("Ali ", " Ahmadi ")]
    public void CanGet_FullName_WhenProvidingProperValues(string fName, string lName)
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = fName,
            LastName = lName
        };

        // Assert
        user.FullName.Should().Be(fName.Trim() + " " + lName.Trim());
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", null)]
    public void FullName_IsNull_WhenBothFirstAndLastNameIsNullOrEmpty(string? fName, string? lName)
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = fName,
            LastName = lName
        };

        // Assert
        user.FullName.Should().BeNull();
    }

    [Fact]
    public void DisplayName_GetsFullName_WhenFullNameIsNotNull()
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = "Ali",
            LastName = "Rezaei"
        };

        // Assert
        user.DisplayName.Should().Be(user.FullName);
    }

    [Fact]
    public void DisplayName_GetsEmail_WhenFullNameIsNull()
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = "",
            LastName = "",
            Email = "example@email.com"
        };

        // Assert
        user.DisplayName.Should().Be("example@email.com");
    }

    [Fact]
    public void DisplayName_GetsUserName_WhenFullNameAndEmailAreNull()
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = "",
            LastName = "",
            Email = null!,
            UserName = "username"
        };

        // Assert
        user.DisplayName.Should().Be("username");
    }

    [Fact]
    public void DisplayName_GetsUnknownUser_WhenFullNameAndEmailAndUserNamesAreNull()
    {
        // Act
        var user = new ApplicationUser()
        {
            FirstName = "",
            LastName = "",
            Email = null!,
            UserName = null!
        };

        // Assert
        user.DisplayName.Should().Be("Unknown User");
    }
}
