using Ardalis.Result;
using MediatR;

namespace DotnetBoilerPlate.Api.Dto.Auth.Login;

public record LoginRequestDto : IRequest<Result<LoginResponseDto>>
{
    public string Username { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}
