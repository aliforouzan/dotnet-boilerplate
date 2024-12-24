using Ardalis.Result;
using MediatR;

namespace DotnetBoilerPlate.Api.Dto.Orders.Create;

public record OrderCreateRequestDto : IRequest<Result>
{
    public int ProductId { get; set; }
    public string OrderType { get; set; } = null!;
    public decimal UnitCount { get; set; }
    public decimal PricePerUnit { get; set; }
}