using System;

namespace DotnetBoilerPlate.Application.Dto.Auth.Login;

public record LoginResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime ExpiredAt { get; set; } = DateTime.MinValue;
}
