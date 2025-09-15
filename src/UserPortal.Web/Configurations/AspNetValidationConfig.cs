using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using UserPortal.UseCases.Validations.DTOs;

namespace UserPortal.Web.Configurations;

public static class AspNetValidationConfig
{
    public static IServiceCollection ConfigureAspNetValidation(this IServiceCollection services)
    {
        // Enable automatic validation and client-side adapters
        services.AddFluentValidationAutoValidation()
               .AddFluentValidationClientsideAdapters();

        // Register all validators from the assembly containing our DTOs
        services.AddValidatorsFromAssemblyContaining<RegistreDtoValidations>();

        return services;
    }
}