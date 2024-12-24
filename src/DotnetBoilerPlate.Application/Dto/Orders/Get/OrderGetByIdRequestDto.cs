using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Text.Json.Serialization;

namespace DotnetBoilerPlate.Application.Dto.Orders.Get;

public record OrderGetByIdRequestDto : IRequest<Result<OrderResponseDto>>, IUserId
{
    [JsonIgnore]
    public UserId UserId { get; set; }
    public int Id { get; set; }
}