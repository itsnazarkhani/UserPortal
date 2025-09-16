using System;
using Microsoft.EntityFrameworkCore;
using UserPortal.Infrastructure.Data;

namespace UserPortal.Web.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services,
            IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' is missing. " +
                "Please configure it in user secrets, environment variables, or appsettings.json.");

        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlite(connectionString));

        return services;
    }
}
