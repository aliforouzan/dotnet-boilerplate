using Mapster;
using MediatR;
using DotnetBoilerPlate.Application.Dto.Geo;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Application.Services.Geo;

public class GeoGetAllProvincesHandler : IRequestHandler<GeoGetAllProvincesRequestDto, PaginatedList<GeoResponseDto>>
{
    private readonly IProvinceRepository _provinceRepository;

    public GeoGetAllProvincesHandler(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    public async Task<PaginatedList<GeoResponseDto>> Handle(GeoGetAllProvincesRequestDto geoGetAllProvincesRequestDto,
        CancellationToken cancellationToken)
    {
        var orders = _provinceRepository.GetAllAsync();

        return await orders.ProjectToType<GeoResponseDto>()
            .ToPaginatedListAsync(geoGetAllProvincesRequestDto.CurrentPage, geoGetAllProvincesRequestDto.PageSize);
    }
}