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

namespace UserPortal.UseCases.Tests.ValidationTests;

public class TestEmailValidationRules : IEmailValidationRules
{
    public string EmptyMessage => "آدرس ایمیل الزامی می‌باشد.";
    public string InvalidMessage => "آدرس ایمیل نامعتبر است.";
}

public class TestPasswordValidationRules : IPasswordValidationRules
{
    public int MinLength => ValidationConstants.Password.MinLength;
    public int MaxLength => ValidationConstants.Password.MaxLength;
    public string Pattern => @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])";
    public string EmptyMessage => "رمز عبور الزامیست.";
    public string LengthMessage => $"رمز عبور باید بین {MinLength} و {MaxLength} کاراکتر باشد.";
    public string PatternMessage => "رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.";
}

public class TestValidationModeConfig : IValidationModeConfig
{
    public CascadeMode RuleLevelCascadeMode => CascadeMode.Stop;
    public CascadeMode ClassLevelCascadeMode => CascadeMode.Continue;
}

public class LoginDtoValidationsTests
{
    private readonly LoginDtoValidations _validator;
    private readonly Faker _faker;
    private readonly char[] SpecialChars = "@#!$%^&*?".ToCharArray();
    private readonly TestEmailValidationRules _emailRules;
    private readonly TestPasswordValidationRules _passwordRules;
    private readonly TestValidationModeConfig _validationMode;

    public LoginDtoValidationsTests()
    {
        _emailRules = new TestEmailValidationRules();
        _passwordRules = new TestPasswordValidationRules();
        _validationMode = new TestValidationModeConfig();
        _validator = new LoginDtoValidations(_emailRules, _passwordRules, _validationMode);
        _faker = new Faker("fa");
    }

    public string GenerateSecurePassword(int length = 12)
    {
        if (length < 4) throw new ArgumentException("length must be at least 4", nameof(length));

        // Ensuer at least one of each requried chars for valid password
        var lower = _faker.Random.Char('a', 'z');
        var upper = _faker.Random.Char('A', 'Z');
        var digit = _faker.Random.Char('0', '9');
        var special = _faker.PickRandom(SpecialChars);

        // Fill the rest randomly from all allowed sets
        var allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#!$%^&*?";
        var remaining = _faker.Random.String2(length - 4, allChars);

        // Combine and shuffle
        var passwordChars = new List<char> { lower, upper, digit, special };
        passwordChars.AddRange(remaining);

        return new string(passwordChars.OrderBy(_ => _faker.Random.Int()).ToArray());
    }

    [Fact]
    public void Should_Not_Have_Error_When_Input_Is_Valid()
    {
        // Arrange
        var dto = new LoginDto(
            _faker.Internet.Email(),
            GenerateSecurePassword(ValidationConstants.Password.MinLength),
            _faker.Random.Bool()
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
            GenerateSecurePassword(ValidationConstants.Password.MinLength),
            _faker.Random.Bool()
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
            GenerateSecurePassword(ValidationConstants.Password.MinLength),
            _faker.Random.Bool()
        );

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("آدرس ایمیل نامعتبر است.");
        result.Errors.Count.Should().Be(1);
    }
}
