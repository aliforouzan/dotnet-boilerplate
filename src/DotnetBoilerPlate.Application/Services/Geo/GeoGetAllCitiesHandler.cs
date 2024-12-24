using Mapster;
using MediatR;
using DotnetBoilerPlate.Application.Dto.Geo;
using DotnetBoilerPlate.Application.Dto.Orders;
using DotnetBoilerPlate.Application.Dto.Orders.Get;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Geo;

public class GeoGetAllCitiesHandler : IRequestHandler<GeoGetAllCitiesRequestDto, PaginatedList<GeoResponseDto>>
{
    private readonly ICityRepository _cityRepository;

    public GeoGetAllCitiesHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<PaginatedList<GeoResponseDto>> Handle(GeoGetAllCitiesRequestDto geoGetAllCitiesRequestDto,
        CancellationToken cancellationToken)
    {
        var orders = _cityRepository.FindAsync(c => c.ProvinceId == geoGetAllCitiesRequestDto.ProvinceId);

        return await orders.ProjectToType<GeoResponseDto>()
            .ToPaginatedListAsync(geoGetAllCitiesRequestDto.CurrentPage, geoGetAllCitiesRequestDto.PageSize);
    }
}