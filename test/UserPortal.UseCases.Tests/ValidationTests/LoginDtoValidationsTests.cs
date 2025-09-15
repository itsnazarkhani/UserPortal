using FluentValidation;
using FluentValidation.TestHelper;
using Bogus;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.DTOs;
using UserPortal.UseCases.Validations.Rules;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Tests.TestDoubles;

namespace UserPortal.UseCases.Tests.ValidationTests;

public class LoginDtoValidationsTests : ValidatorTestBase
{
    private readonly LoginDtoValidations _validator;

    public LoginDtoValidationsTests()
    {
        _validator = new LoginDtoValidations(EmailRules, PasswordRules, ValidationMode);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Input_Is_Valid()
    {
        // Arrange
        var dto = new LoginDto(
            Faker.Internet.Email(),
            PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
            Faker.Random.Bool()
        );

        // Act 
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var dto = new LoginDto(
            string.Empty,
            PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
            Faker.Random.Bool()
        );

        // Act 
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("آدرس ایمیل الزامی می‌باشد.");
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Email_Is_Invalid()
    {
        // Arrange
        var dto = new LoginDto(
            "invalid-email",
            PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
            Faker.Random.Bool()
        );

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("آدرس ایمیل نامعتبر است.");
        result.Errors.Count.Should().Be(1);
    }
}
