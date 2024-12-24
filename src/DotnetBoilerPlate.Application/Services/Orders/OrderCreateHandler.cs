using Ardalis.Result;
using DotnetBoilerPlate.Application.Dto.Orders;
using DotnetBoilerPlate.Application.Dto.Orders.Create;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using Mapster;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Orders;

public class OrderCreateHandler : IRequestHandler<OrderCreateRequestDto, Result<OrderResponseDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreateHandler(IOrderRepository orderRepository, IProductRepository productRepository,IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OrderResponseDto>> Handle(OrderCreateRequestDto orderCreateRequestPlusUserIdDto,
        CancellationToken cancellationToken)
    {
        Product? product;
        try
        {
            product =
                await _productRepository.GetByIdAsync(orderCreateRequestPlusUserIdDto.ProductId);
            if (product is null)
            {
                return Result.NotFound("کالای ارسالی یافت نشد");
            }
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }

        Order order = new(
            orderCreateRequestPlusUserIdDto.UserId,
            orderCreateRequestPlusUserIdDto.ProductId,
            orderCreateRequestPlusUserIdDto.Type.ToString(),
            orderCreateRequestPlusUserIdDto.UnitCount,
            orderCreateRequestPlusUserIdDto.PricePerUnit
        );

        try
        {
            order.IsProcessable(product);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }

        /* create order */
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Create order
            var orderRepo = _unitOfWork.GetRepository<Order>();
            
            orderRepo.Add(order);

            // Commit transaction
            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return CustomError.DbError;
        }

        return order.Adapt<OrderResponseDto>();
    }
}