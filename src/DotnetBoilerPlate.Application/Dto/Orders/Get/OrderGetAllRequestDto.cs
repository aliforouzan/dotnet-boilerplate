using MediatR;
using DotnetBoilerPlate.Application.Filters.Res;
using DotnetBoilerPlate.Domain.Entities.Common;
using DotnetBoilerPlate.Domain.Interfaces;
using DotnetBoilerPlate.Shared.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Text.Json.Serialization;

namespace DotnetBoilerPlate.Application.Dto.Orders.Get;

public record OrderGetAllRequestDto : IRequest<PaginatedList<OrderResponseDto>>, IUserId, IPagination 
{
    [JsonIgnore]
    public UserId UserId { get; set; }
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
}