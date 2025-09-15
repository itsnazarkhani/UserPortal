using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserPortal.UseCases.Validations.DTOs;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.Configurations;

public static class ValidationConfig
{
    public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
    {
        // Register validation rule providers
        services.AddSingleton<IEmailValidationRules, DefaultEmailValidationRules>();
        services.AddSingleton<IPasswordValidationRules, DefaultPasswordValidationRules>();
        services.AddSingleton<IValidationModeConfig, DefaultValidationModeConfig>();

        // Configure global validation options
        var validationConfig = new DefaultValidationModeConfig();
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = validationConfig.ClassLevelCascadeMode;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = validationConfig.RuleLevelCascadeMode;

        return services;
    }
}
