using Ardalis.Result;
using MediatR;

namespace DotnetBoilerPlate.Api.Dto.Orders.Update;

public record OrderUpdateRequestDto : IRequest<Result>
{
    public decimal UnitCount { get; set; }
    public decimal PricePerUnit { get; set; }
}