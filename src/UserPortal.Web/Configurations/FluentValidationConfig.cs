using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.DTOs;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.Web.Configurations;

public static class FluentValidationConfig
{
    public static IServiceCollection ConfigureAspNetValidation(this IServiceCollection services)
    {
        // Enable automatic validation and client-side adapters
        services.AddFluentValidationAutoValidation()
               .AddFluentValidationClientsideAdapters();

        // Register all validators from the assembly containing our DTOs
        services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

        return services;
    }

      public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
    {
        // Register validation rule providers
        services.AddSingleton<IEmailValidationRules, DefaultEmailValidationRules>();
        services.AddSingleton<IPasswordValidationRules, DefaultPasswordValidationRules>();
        services.AddSingleton<IValidationModeConfig, DefaultValidationModeConfig>();
        services.AddSingleton<IUserNameValidationRules, DefaultUserNameValidationRules>();

        // Configure global validation options
        var validationConfig = new DefaultValidationModeConfig();
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = validationConfig.ClassLevelCascadeMode;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = validationConfig.RuleLevelCascadeMode;

        return services;
    }
}