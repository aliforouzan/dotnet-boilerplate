using MediatR;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Statics;

namespace DotnetBoilerPlate.Application.Dto.Geo;

public record GeoGetAllCitiesRequestDto : IRequest<PaginatedList<GeoResponseDto>>, IPagination 
{
    public int ProvinceId;
    public int? CurrentPage { get; set; } = Pagination.DefaultCurrentPage;
    public int? PageSize { get; set; } = Pagination.DefaultPageSize;
}