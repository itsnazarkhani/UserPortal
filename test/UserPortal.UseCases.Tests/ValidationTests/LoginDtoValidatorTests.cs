using FluentValidation.TestHelper;
using Bogus;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.DTOs;
using FluentAssertions;
using UserPortal.UseCases.Tests.TestDoubles;

namespace UserPortal.UseCases.Tests.ValidationTests;

// TODO: Decouple tests for email and password rules to separate classes
public class LoginDtoValidatorTests : ValidatorTestBase
{
    private readonly LoginDtoValidator _validator;

    public LoginDtoValidatorTests()
    {
        _validator = new LoginDtoValidator(EmailRules, PasswordRules, ValidationMode);
    }

    private LoginDto GetLoginDtoWithEmail(string email) =>
        new LoginDto(
            email,
            PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
            Faker.Random.Bool()
        );

    private LoginDto GetLoginDtoWithPassword(string password) =>
        new LoginDto(
            Faker.Internet.Email(),
            password,
            Faker.Random.Bool()
        );

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
        var dto = GetLoginDtoWithEmail(string.Empty);

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
        var dto = GetLoginDtoWithEmail("invalid-email");

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("آدرس ایمیل نامعتبر است.");
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Password_Is_Empty()
    {
        // Arrange 
        var dto = GetLoginDtoWithPassword(string.Empty);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(PasswordRules.EmptyMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Password_Is_Too_Short()
    {
        if (ValidationConstants.Password.MinLength > 0)
        {
            // Arrange
            var dto = GetLoginDtoWithPassword(PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength - 1));
            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(PasswordRules.LengthMessage);
            result.Errors.Count.Should().Be(1);
        }

        // Only when the MinLength is 0 or negative
        Assert.True(true);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Password_Is_Too_Long()
    {
        if (ValidationConstants.Password.MaxLength > 0 && ValidationConstants.Password.MaxLength < int.MaxValue)
        {
            // Arrange
            var dto = GetLoginDtoWithPassword(PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MaxLength + 1));

            // Act
            var result = _validator.TestValidate(dto);

            // Assert 
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(PasswordRules.LengthMessage);
            result.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("34234244424242")]  // Just numbers
    [InlineData("fehgoihgoiewhg")]  // Just lowercase
    [InlineData("FIHEWOIAGHEWIO")]  // Just uppercase
    [InlineData("&@%*&^$*&@&*$?")]  // Just special characters
    [InlineData("feigHIOE#@$@OH")]  // Without any number
    [InlineData("342424GALKHE#$")]  // Without any lowercase
    [InlineData("343heigeio$@#?")]  // Without any uppercase
    [InlineData("e535fehioGOIEW")]  // Without any special characters
    public void Should_Have_Validation_Error_When_Password_DoesNot_Follow_Valid_Pattern(string password)
    {
        // Arrange
        var dto = GetLoginDtoWithPassword(password);

        // Act
        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(PasswordRules.PatternMessage);
        result.Errors.Count.Should().Be(1);
    }
}
