using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Application.Dto.Auth.Register;

public record RegisterResponseDto
{
    public UserId? Id { get; set; } = null!;
}
