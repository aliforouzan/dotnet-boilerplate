using Ardalis.Result;
using MediatR;
using DotnetBoilerPlate.Application.Dto.Orders;

namespace DotnetBoilerPlate.Api.Dto.Orders.Get;

public record OrderGetByIdRequestDto : IRequest<Result<OrderResponseDto>>
{
    public int Id { get; set; }
}