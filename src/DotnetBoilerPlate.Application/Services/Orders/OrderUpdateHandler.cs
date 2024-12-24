using Ardalis.Result;
using DotnetBoilerPlate.Application.Dto.Orders.Update;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Orders;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateRequestDto, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderUpdateHandler(IOrderRepository orderRepository, IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(OrderUpdateRequestDto orderUpdateRequestDto,
        CancellationToken cancellationToken)
    {
        Order? order;
        Product? product;
        try
        {
            order = await _orderRepository.GetByIdAsync(orderUpdateRequestDto.Id);
            if (order is null)
            {
                return Result.NotFound("سفارش یافت نشد");
            }

            if (!order.IsEditable())
            {
                return Result.Forbidden("سفارش در وضعیت قابل ویرایش نیست");
            }

            product = await _productRepository.GetByIdAsync(order.ProductId);
            if (product is null)
            {
                /* this shouldn't happen */
                return CustomError.InternalServerError;
            }
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }

        /* update order */
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            order.UnitCount = orderUpdateRequestDto.UnitCount;
            order.PricePerUnit = orderUpdateRequestDto.PricePerUnit;
            order.UpdatedAt = DateTime.Now;

            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.CriticalError("خطا در اجرای درخواست");
        }
    }
}