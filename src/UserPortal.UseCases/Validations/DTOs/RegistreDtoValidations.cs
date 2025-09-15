using System;
using System.Data;
using FluentValidation;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.DTOs;

public class RegistreDtoValidations : AbstractValidator<RegisterDto>
{
    public RegistreDtoValidations(
        IUserNameValidationRules userNameRules,
        IEmailValidationRules emailRules,
        IPasswordValidationRules passwordRules,
        IValidationModeConfig validationMode
    )
    {
        RuleLevelCascadeMode = validationMode.RuleLevelCascadeMode;
        ClassLevelCascadeMode = validationMode.ClassLevelCascadeMode;

        // UserName
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(userNameRules.EmptyMessage)
            .Length(userNameRules.MinLength, userNameRules.MaxLength)
            .WithMessage(userNameRules.LengthMessage)
            .Matches(userNameRules.Pattern)
            .WithMessage(userNameRules.PatternMessage)
            .Must(name => Array.IndexOf(userNameRules.InvalidUserNames, name) == -1)
            .WithMessage(userNameRules.InvalidUserNameMessage);

        // Email
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(emailRules.EmptyMessage)
            .EmailAddress()
            .WithMessage(emailRules.InvalidMessage);

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(passwordRules.EmptyMessage)
            .Length(passwordRules.MinLength, passwordRules.MaxLength)
            .WithMessage(passwordRules.LengthMessage)
            .Matches(passwordRules.Pattern)
            .WithMessage(passwordRules.PatternMessage);
    }
}
