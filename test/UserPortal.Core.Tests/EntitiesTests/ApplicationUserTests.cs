using System;
using Bogus;
using FluentAssertions;
using UserPortal.Core.Entities;

namespace UserPortal.Core.Tests.EntitiesTests;

public class ApplicationUserTests
{
    [Fact]
    public void CanGet_FullName_WhenProvidingProperValues()
    {
        // Arrange
        var faker = new Faker("fa");
        string fName, lName;
        fName = faker.Name.FirstName();
        lName = faker.Name.LastName();

        // Act
        var user = new User()
        {
            FirstName = fName,
            LastName = lName
        };

        // Assert
        user.FullName.Should().Be(fName.Trim() + " " + lName.Trim());
    }
}
