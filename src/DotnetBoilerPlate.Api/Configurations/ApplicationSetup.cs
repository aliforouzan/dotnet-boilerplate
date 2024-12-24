using Microsoft.Extensions.DependencyInjection;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Factory;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;

namespace DotnetBoilerPlate.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        services.AddScoped<IContextFactory, ContextFactory>();
        return services;
    }
}