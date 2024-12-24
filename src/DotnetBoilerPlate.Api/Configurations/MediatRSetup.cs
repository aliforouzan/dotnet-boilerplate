using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DotnetBoilerPlate.Application.Filters.req.Behaviors;

namespace DotnetBoilerPlate.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(Application.IAssemblyMarker));
            config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
        });
        
        services.AddHttpContextAccessor();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(InjectUserIdPipelineBehavior<,>));



        return services;
    }
}