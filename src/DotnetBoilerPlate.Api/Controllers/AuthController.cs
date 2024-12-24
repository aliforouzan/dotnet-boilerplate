using Mapster;
using MediatR;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DotnetBoilerPlate.Api.Dto.Auth.Login;
using DotnetBoilerPlate.Api.Dto.Auth.Register;
using ApplicationLayerLoginRequestDto = DotnetBoilerPlate.Application.Dto.Auth.Login.LoginRequestDto;
using ApplicationLayerRegisterRequestDto = DotnetBoilerPlate.Application.Dto.Auth.Register.RegisterRequestDto;
using ApplicationLayerMeRequestDto = DotnetBoilerPlate.Application.Dto.Auth.Me.MeRequestDto;


namespace DotnetBoilerPlate.Api.Controllers;

public static class AuthController
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("api/auth")
            .WithTags("Auth");

        group.MapPost("/login", async (IMediator mediator, LoginRequestDto loginRequestDto) =>
        {
            var result = await mediator.Send(loginRequestDto.Adapt<ApplicationLayerLoginRequestDto>());
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/register", async (IMediator mediator, RegisterRequestDto registerRequestDto) =>
        {
            var result = await mediator.Send(registerRequestDto.Adapt<ApplicationLayerRegisterRequestDto>());
            return result.ToMinimalApiResult();
        });
        
        group.MapGet("/me",
            async (IMediator mediator) =>
            {
                var result = await mediator.Send(new ApplicationLayerMeRequestDto());

                return result.ToMinimalApiResult();
            }).RequireAuthorization();
    }
}