using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Api.Dto.Auth.Me;

public record MeResponseDto
{
    public UserId Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string NationalCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
    
}