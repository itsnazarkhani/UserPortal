using System;
using FluentValidation;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.Extensions;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> ApplyEmailRules<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IEmailValidationRules emailRules) =>
            ruleBuilder
                .NotEmpty().WithMessage(emailRules.EmptyMessage)
                .EmailAddress().WithMessage(emailRules.InvalidMessage);

    public static IRuleBuilderOptions<T, string> ApplyPasswordRules<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IPasswordValidationRules passwordRules) =>
            ruleBuilder
                .NotEmpty()
                .WithMessage(passwordRules.EmptyMessage)
                .Length(passwordRules.MinLength, passwordRules.MaxLength)
                .WithMessage(passwordRules.LengthMessage)
                .Matches(passwordRules.Pattern)
                .WithMessage(passwordRules.PatternMessage);

    public static IRuleBuilderOptions<T, string> ApplyUserNameRules<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IUserNameValidationRules userNameRules) =>
            ruleBuilder
                .NotEmpty()
                .WithMessage(userNameRules.EmptyMessage)
                .Length(userNameRules.MinLength, userNameRules.MaxLength)
                .WithMessage(userNameRules.LengthMessage)
                .Matches(userNameRules.Pattern)
                .WithMessage(userNameRules.PatternMessage)
                .Must(name => Array.IndexOf(userNameRules.InvalidUserNames, name) == -1)
                .WithMessage(userNameRules.InvalidUserNameMessage);
}
