using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Application.Dto.Orders.Create;

public record OrderCreateRequestDto : IRequest<Result<OrderResponseDto>>, IUserId
{
    public UserId UserId { get; set; }
    
    public int ProductId { get; set; }

    public OrderType Type { get; set; }

    public decimal UnitCount { get; set; }

    public decimal PricePerUnit { get; set; }
}