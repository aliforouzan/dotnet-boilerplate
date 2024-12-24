using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Api.Dto.Auth.Me;

public record MeRequestDto : IRequest<Result<MeResponseDto>>, IUserId
{
    [JsonIgnore]
    public UserId UserId { get; set; }
}