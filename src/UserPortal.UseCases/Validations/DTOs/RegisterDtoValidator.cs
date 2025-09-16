using System;
using System.Data;
using FluentValidation;
using UserPortal.Core.Constants;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.Extensions;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.DTOs;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator(
        IUserNameValidationRules userNameRules,
        IEmailValidationRules emailRules,
        IPasswordValidationRules passwordRules,
        IValidationModeConfig validationMode
    )
    {
        RuleLevelCascadeMode = validationMode.RuleLevelCascadeMode;
        ClassLevelCascadeMode = validationMode.ClassLevelCascadeMode;

        // UserName
        RuleFor(x => x.UserName).ApplyUserNameRules(userNameRules);
        // Email
        RuleFor(x => x.Email).ApplyEmailRules(emailRules);
        // Password
        RuleFor(x => x.Password).ApplyPasswordRules(passwordRules);
    }
}
