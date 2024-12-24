using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using DotnetBoilerPlate.Application.Dto.Auth.Login;
using DotnetBoilerPlate.Domain.Auth;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Domain.Interfaces.Auth;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Auth;

public class LoginHandler : IRequestHandler<LoginRequestDto, Result<LoginResponseDto>>
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILoginService _loginService;

    public LoginHandler(IUserRepository userRepository, IConfiguration config, ITokenService tokenService,
        ILoginService loginService)
    {
        _config = config;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _loginService = loginService;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken)
    {
        User? user;

        try
        {
            user = loginRequestDto.Username.Length switch
            {
                10 => await _userRepository.GetByNationalCodeAsync(loginRequestDto.Username),
                11 => await _userRepository.GetByPhoneNumberAsync(loginRequestDto.Username),
                _ => null
            };
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }


        try
        {
            _loginService.Login(user, loginRequestDto.Password);
        }
        catch (AuthenticationException e)
        {
            return Result.Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            return CustomError.InternalServerError;
        }


        try
        {
            var token = new
            {
                AccessToken = _tokenService.CreateToken(user, TokenType.Access),
                RefreshToken = _tokenService.CreateToken(user, TokenType.Refresh),
                ExpiredAt = DateTime.Now.AddSeconds(double.Parse(_config["JWT:AccessTokenExpireDuration"]))
            };

            return token.Adapt<LoginResponseDto>();
        }
        catch (Exception e)
        {
            return CustomError.InternalServerError;
        }
    }
}