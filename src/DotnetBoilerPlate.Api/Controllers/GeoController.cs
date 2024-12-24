using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DotnetBoilerPlate.Shared.Types;
using ApplicationLayerGeoGetAllCitiesRequestDto = DotnetBoilerPlate.Application.Dto.Geo.GeoGetAllCitiesRequestDto;
using ApplicationLayerGeoGetAllProvincesRequestDto = DotnetBoilerPlate.Application.Dto.Geo.GeoGetAllProvincesRequestDto;

namespace DotnetBoilerPlate.Api.Controllers;

public static class GeoController
{
    public static void MapGeoEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("api/geo/provinces")
            .WithTags("geo");

        group.MapGet("/", async (IMediator mediator, [AsParameters] PaginationParams pagination) =>
        {
            var result = await mediator.Send(pagination.Adapt<ApplicationLayerGeoGetAllProvincesRequestDto>());
            return result;
        });
        
        group.MapGet("/{id:int}/cities", async (IMediator mediator, int id) =>
        {
            var result = await mediator.Send(new ApplicationLayerGeoGetAllCitiesRequestDto { ProvinceId = id });
            return result;
        });
    }
}