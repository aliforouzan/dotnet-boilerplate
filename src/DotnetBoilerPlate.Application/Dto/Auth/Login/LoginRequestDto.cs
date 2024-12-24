using Ardalis.Result;
using MediatR;
using LoginResponseDto = DotnetBoilerPlate.Application.Dto.Auth.Login.LoginResponseDto;

namespace DotnetBoilerPlate.Application.Dto.Auth.Login;

public record LoginRequestDto : IRequest<Result<LoginResponseDto>>
{
    public string Username { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}
