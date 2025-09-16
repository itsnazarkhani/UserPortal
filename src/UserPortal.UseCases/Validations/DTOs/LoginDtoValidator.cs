using System;
using FluentValidation;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.Extensions;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.DTOs;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator(
        IEmailValidationRules emailRules,
        IPasswordValidationRules passwordRules,
        IValidationModeConfig validationMode)
    {
        RuleLevelCascadeMode = validationMode.RuleLevelCascadeMode;
        ClassLevelCascadeMode = validationMode.ClassLevelCascadeMode;

        // Email
        RuleFor(x => x.Email).ApplyEmailRules(emailRules);
        // Password
        RuleFor(x => x.Password).ApplyPasswordRules(passwordRules);
    }
}
