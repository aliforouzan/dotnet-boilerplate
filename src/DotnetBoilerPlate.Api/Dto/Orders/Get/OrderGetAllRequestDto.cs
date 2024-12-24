using MediatR;
using DotnetBoilerPlate.Application.Dto.Orders;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Domain.Interfaces;
using DotnetBoilerPlate.Shared.Interfaces;

namespace DotnetBoilerPlate.Api.Dto.Orders.Get;

public record OrderGetAllRequestDto : IRequest<PaginatedList<OrderResponseDto>>, IPagination 
{
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
}