using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using DotnetBoilerPlate.Shared.Interfaces;

namespace DotnetBoilerPlate.Application.Filters.req.Behaviors;

public class InjectUserIdPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IUserId
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InjectUserIdPipelineBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            request.UserId = new Guid(userId);
        }

        return await next();
    }
}