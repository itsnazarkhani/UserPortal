using System;
using FluentValidation;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.DTOs;

public class LoginDtoValidations : AbstractValidator<LoginDto>
{
    public LoginDtoValidations(
        IEmailValidationRules emailRules,
        IPasswordValidationRules passwordRules,
        IValidationModeConfig validationMode)
    {
        RuleLevelCascadeMode = validationMode.RuleLevelCascadeMode;
        ClassLevelCascadeMode = validationMode.ClassLevelCascadeMode;
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(emailRules.EmptyMessage)
            .EmailAddress()
            .WithMessage(emailRules.InvalidMessage);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(passwordRules.EmptyMessage)
            .Length(passwordRules.MinLength, passwordRules.MaxLength)
            .WithMessage(passwordRules.LengthMessage)
            .Matches(passwordRules.Pattern)
            .WithMessage(passwordRules.PatternMessage);
    }
}
