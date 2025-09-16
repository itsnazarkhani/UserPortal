using System;
using FluentAssertions;
using FluentValidation.TestHelper;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Tests.TestDoubles;
using UserPortal.UseCases.Validations.DTOs;

namespace UserPortal.UseCases.Tests.ValidationTests;

public class RegisterDtoValidatorTests : ValidatorTestBase
{
    private readonly RegisterDtoValidator _validator;

    public RegisterDtoValidatorTests()
    {
        _validator = new RegisterDtoValidator(UserNameRules, EmailRules, PasswordRules, ValidationMode);
    }

    private RegisterDto GetRegisterDtoWithUserName(string userName) =>
        new RegisterDto(
            userName,
            Faker.Internet.Email(),
            PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
            Faker.Random.Bool()
        );

    private RegisterDto GetRegisterDtoWithPassword(string password) =>
        new RegisterDto(
            Faker.Internet.UserName(),
            Faker.Internet.Email(),
            password,
            Faker.Random.Bool()
        );

    private RegisterDto GetRegisterDtoWithEmail(string email) =>
       new RegisterDto(
           Faker.Internet.UserName(),
           email,
           PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength),
           Faker.Random.Bool()
       );

    [Fact]
    public void Should_Not_Have_Error_When_Input_Is_Valid()
    {
        // Arrange 
        var dto = new RegisterDto(
            Faker.Internet.UserName(),
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
        var dto = GetRegisterDtoWithEmail(string.Empty);

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
        var dto = GetRegisterDtoWithEmail("invalid-email");

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
        var dto = GetRegisterDtoWithPassword(string.Empty);

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
            var dto = GetRegisterDtoWithPassword(
                PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MinLength - 1));

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
            var dto = GetRegisterDtoWithPassword(
                PasswordGenerator.GenerateSecurePassword(ValidationConstants.Password.MaxLength + 1));

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
        var dto = GetRegisterDtoWithPassword(password);

        // Act
        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(PasswordRules.PatternMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_UserName_Is_Empty()
    {
        // Arrange
        var dto = GetRegisterDtoWithUserName(string.Empty);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage(UserNameRules.EmptyMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_UserName_Is_Too_Short()
    {
        // Arrange
        var dto = GetRegisterDtoWithUserName(
            Faker.Lorem.Letter(ValidationConstants.UserName.MinLength - 1));

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage(UserNameRules.LengthMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Fact]
    public void Should_Have_Validation_Error_When_UserName_Is_Too_Long()
    {
        // Arrange
        var dto = GetRegisterDtoWithUserName(
            Faker.Lorem.Letter(ValidationConstants.UserName.MaxLength + 1));

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage(UserNameRules.LengthMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Theory]
    [InlineData("_user")]   // Starts with underscore
    [InlineData("user_")]   // Ends with underscore
    [InlineData("user..name")]  // Double dots
    [InlineData("user__name")]  // Double underscores
    [InlineData(".user")]   // Starts with dot
    [InlineData("user.")]   // Ends with dot
    [InlineData("user name")]   // Has white space
    [InlineData("علی")] // Non-ASCII characters
    public void Should_Have_Error_When_UserName_DoesNot_Match_Valid_Pattern(string userName)
    {
        // Arrange
        var dto = GetRegisterDtoWithUserName(userName);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage(UserNameRules.PatternMessage);
        result.Errors.Count.Should().Be(1);
    }

    [Theory]
    [InlineData("Anonymous")]
    [InlineData("Unknown")]
    public void Should_Have_Error_When_UserName_Is_Forbidden_One(string userName)
    {
        // Arrange
        var dto = GetRegisterDtoWithUserName(userName);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage(UserNameRules.InvalidUserNameMessage);
        result.Errors.Count.Should().Be(1);
    }
}
