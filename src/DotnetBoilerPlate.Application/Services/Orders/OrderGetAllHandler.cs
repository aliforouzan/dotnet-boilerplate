using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using DotnetBoilerPlate.Application.Dto.Orders;
using DotnetBoilerPlate.Application.Dto.Orders.Get;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using System;

namespace DotnetBoilerPlate.Application.Services.Orders;

public class OrderGetAllHandler : IRequestHandler<OrderGetAllRequestDto, PaginatedList<OrderResponseDto>>
{
    private readonly IOrderRepository _orderRepository;

    public OrderGetAllHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedList<OrderResponseDto>> Handle(OrderGetAllRequestDto orderGetAllRequestDto,
        CancellationToken cancellationToken)
    {
        var orders = _orderRepository.GetAllAsync(orderGetAllRequestDto.UserId);

        return await orders.ProjectToType<OrderResponseDto>()
            .ToPaginatedListAsync(orderGetAllRequestDto.CurrentPage, orderGetAllRequestDto.PageSize);
    }
}