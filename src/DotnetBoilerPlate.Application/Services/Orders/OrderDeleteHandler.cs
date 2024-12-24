using Ardalis.Result;
using DotnetBoilerPlate.Application.Dto.Orders.Delete;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Statics;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Orders;

public class OrderDeleteHandler : IRequestHandler<OrderDeleteRequestDto, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderDeleteHandler(IOrderRepository orderRepository, IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(OrderDeleteRequestDto orderDeleteRequestDto, CancellationToken cancellationToken)
    {
        Order? order;
        Product? product;
        try
        {
            order = await _orderRepository.GetByIdAsync(orderDeleteRequestDto.Id);
            if (order is null)
            {
                return Result.NotFound("سفارش یافت نشد");
            }

            if (!order.IsDeletable())
            {
                return Result.Forbidden("وضعیت سفارش در وضعیت قابل لغو نیست");
            }

            product = await _productRepository.GetByIdAsync(order.ProductId);
            var irrProduct = await _productRepository.GetBySymbolAsync("IRR");
            if (product is null || irrProduct is null)
            {
                /* this shouldn't happen */
                return CustomError.InternalServerError;
            }
        }
        catch (Exception e)
        {
            return CustomError.DbError;
        }

        /* delete order */
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            order.Status = OrderStatus.Cancelled.ToString();
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