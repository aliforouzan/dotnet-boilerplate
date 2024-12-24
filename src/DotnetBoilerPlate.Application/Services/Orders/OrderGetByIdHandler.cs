using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Application.Dto.Orders;
using DotnetBoilerPlate.Application.Dto.Orders.Get;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

namespace DotnetBoilerPlate.Application.Services.Orders;

public class OrderGetByIdHandler : IRequestHandler<OrderGetByIdRequestDto, Result<OrderResponseDto>>
{
    private readonly IOrderRepository _orderRepository;

    public OrderGetByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponseDto>> Handle(OrderGetByIdRequestDto orderGetByIdRequestDto,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(orderGetByIdRequestDto.Id, orderGetByIdRequestDto.UserId);
        if (order is null)
        {
            return Result.NotFound("سفارش یافت نشد");
        }

        return order.Adapt<OrderResponseDto>();
    }
}