using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Text.Json.Serialization;
using MeResponseDto = DotnetBoilerPlate.Application.Dto.Auth.Me.MeResponseDto;

namespace DotnetBoilerPlate.Application.Dto.Auth.Me;

public record MeRequestDto : IRequest<Result<MeResponseDto>>, IUserId
{
    [JsonIgnore]
    public UserId UserId { get; set; }
}