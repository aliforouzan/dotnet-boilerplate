using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Text.Json.Serialization;

namespace DotnetBoilerPlate.Application.Dto.Orders.Update;

public record OrderUpdateRequestDto : IRequest<Result>, IUserId
{
    public UserId UserId { get; set; }
    public int Id { get; set; }
    public decimal UnitCount { get; set; }
    public decimal PricePerUnit { get; set; }
}