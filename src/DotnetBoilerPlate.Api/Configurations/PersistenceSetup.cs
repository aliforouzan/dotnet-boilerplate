using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EntityFramework.Exceptions.PostgreSQL;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Api.Configurations;

public interface ISession : IUserId
{
    public new UserId UserId { get; }

    public DateTime Now { get; }
}

public class Session : ISession
{
    public UserId UserId { get; set; }
    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if(nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }
}
public static class PersistenceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ISession, Session>();
        // services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            o.UseExceptionProcessor();
        });
        
        services.AddDbContextFactory<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            o.UseExceptionProcessor();
        });

        return services;
    }
}