using Ardalis.Result.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DotnetBoilerPlate.Api.Dto.Orders.Create;
using DotnetBoilerPlate.Api.Dto.Orders.Update;
using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Shared.Types;
using System;
using ApplicationLayerOrderCreateRequestDto = DotnetBoilerPlate.Application.Dto.Orders.Create.OrderCreateRequestDto;
using ApplicationLayerOrderGetAllRequestDto = DotnetBoilerPlate.Application.Dto.Orders.Get.OrderGetAllRequestDto;
using ApplicationLayerOrderGetByIdRequestDto = DotnetBoilerPlate.Application.Dto.Orders.Get.OrderGetByIdRequestDto;
using ApplicationLayerOrderDeleteRequestDto = DotnetBoilerPlate.Application.Dto.Orders.Delete.OrderDeleteRequestDto;
using ApplicationLayerOrderUpdateRequestDto = DotnetBoilerPlate.Application.Dto.Orders.Update.OrderUpdateRequestDto;

namespace DotnetBoilerPlate.Api.Controllers;

public static class OrderController
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("api/orders")
            .WithTags("orders");
        
        group.MapPost("/", async (IMediator mediator, OrderCreateRequestDto orderCreateRequestDto) =>
        {
            OrderType orderType;
            
            var applicationLayerOrderCreateRequestDto = orderCreateRequestDto.Adapt<ApplicationLayerOrderCreateRequestDto>();
            if (Enum.TryParse(orderCreateRequestDto.OrderType, out orderType) == false)
            {
                return Results.BadRequest(new { Message = "نوع سفارش معتبر نیست" });
            }
            applicationLayerOrderCreateRequestDto.Type = orderType;
            
            var result = await mediator.Send(applicationLayerOrderCreateRequestDto);
            return result.ToMinimalApiResult();
        }).RequireAuthorization();
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] PaginationParams pagination) =>
        {
            var result = await mediator.Send(pagination.Adapt<ApplicationLayerOrderGetAllRequestDto>());
            return result;
        }).RequireAuthorization();
        
        group.MapGet("{id}", async (IMediator mediator, int id) =>
        {
            var result = await mediator.Send(new ApplicationLayerOrderGetByIdRequestDto{Id = id});
            return result.ToMinimalApiResult();
        }).RequireAuthorization();
 
        
        group.MapPut("{id}", async (IMediator mediator, int id, OrderUpdateRequestDto orderUpdateRequestDto) =>
        {
            var applicationLayerOrderUpdateRequestDto = orderUpdateRequestDto.Adapt<ApplicationLayerOrderUpdateRequestDto>();
            applicationLayerOrderUpdateRequestDto.Id = id;
            
            var result = await mediator.Send(applicationLayerOrderUpdateRequestDto);
            return result.ToMinimalApiResult();
        }).RequireAuthorization();

        group.MapDelete("{id}", async (IMediator mediator, int id) =>
        {
            var result = await mediator.Send(new ApplicationLayerOrderDeleteRequestDto{Id = id});
            return result.ToMinimalApiResult();
        }).RequireAuthorization();
    }
}