using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Api.Dto.Auth.Register;

public record RegisterResponseDto
{
    public UserId? Id { get; set; } = null!;
}
