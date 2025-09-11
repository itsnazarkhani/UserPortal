using System;
using System.Globalization;
using System.Net.Sockets;
using Bogus;
using FluentAssertions;
using UserPortal.Core.ValueObjects;
using Xunit.Sdk;

namespace UserPortal.Core.Tests.ValueObjectsTests;

public class EmailTests
{
    Faker _faker;

    public EmailTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public void Create_ShuoldTrimAndLowercaseEmail_AndTrimSubjectAndBody()
    {
        // Arrange
        var email = " Test@Email.COM";
        var subject = " Test Subject ";
        var body = " Test body";

        // Act
        var result = Email.Create(email, subject, body);

        // Assert
        result.ReceiverEmailAddress.Should().Be("test@email.com");
        result.Subject.Should().Be("Test Subject");
        result.Body.Should().Be("Test body");
    }

    [Fact]
    public void Create_ShouldHandleNullBody()
    {
        // Arrange
        string email = _faker.Internet.Email();
        string subject = _faker.Lorem.Sentence();

        // Act
        var result = Email.Create(email, subject);

        // Assert
        result.Body.Should().Be(string.Empty);
    }

    [Fact]
    public void WithBody_ShouldReturnNewEmailWithUpdatedBody()
    {
        // Arrange
        var email = Email.Create(_faker.Internet.Email(), "Subject", "Old Body");

        // Act
        var updated = email.WithBody("New Body");

        // Assert
        updated.Body.Should().Be("New Body");
        updated.ReceiverEmailAddress.Should().Be(email.ReceiverEmailAddress);
        updated.Subject.Should().Be(email.Subject);
        updated.Should().NotBeEquivalentTo(email); // ensures immutability
    }

    [Theory]
    [InlineData("a@b.com", "A@B.com", true)]
    [InlineData("a@b.com", "a@b.com", true)]
    [InlineData("a@b.com", "b@a.com", false)]
    [InlineData(null, null, true)]
    [InlineData(null, "a@b.com", false)]
    public void AreEmailAddressesEquivalent_ShouldCompareEmailsCorrectly(string? email1, string? email2, bool expected)
    {
        // Act
        var result = Email.AreEmailAddressesEquivalent(email1, email2);

        // Assert
        result.Should().Be(expected);
    }
}
