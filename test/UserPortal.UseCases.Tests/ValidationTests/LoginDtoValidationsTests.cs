using FluentValidation.TestHelper;
using System;
using Bogus;
using Microsoft.VisualBasic;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.DTOs;

namespace UserPortal.UseCases.Tests.ValidationTests;

public class LoginDtoValidationsTests
{
    private readonly LoginDtoValidations _validator;
    private readonly Faker _faker;
    private readonly Faker<LoginDto> _loginDtoFaker;
    private readonly char[] SpecialChars = "@#!$%^&*?".ToCharArray();

    public LoginDtoValidationsTests()
    {
        _validator = new LoginDtoValidations();

        _faker = new Faker("fa");

        _loginDtoFaker = new Faker<LoginDto>("fa")
            .RuleFor(l => l.Email, f => f.Internet.Email())
            .RuleFor(l => l.Password, f => GenerateSecurePassword(ValidationConstants.Password.MinLength));
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
        var dto = _loginDtoFaker.Generate();

        // Act 
        var result = _validator.Validate(dto);

        // Assert
        // result.ShouldNotHaveAnyValidationErrors();
    }
}
