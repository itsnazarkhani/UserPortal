using System;
using UserPortal.Core.Interfaces;
using UserPortal.Infrastructure.Data.Repositories;
using UserPortal.Infrastructure.Data.UnitOfWork;

namespace UserPortal.Web.Configurations;

public static class RepositoryConfig
{
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
