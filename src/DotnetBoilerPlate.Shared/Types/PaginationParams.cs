using DotnetBoilerPlate.Shared.Interfaces;

namespace DotnetBoilerPlate.Shared.Types;

public record PaginationParams : IPagination
{
    public int? CurrentPage { get; set; } = 0;
    public int? PageSize { get; set; } = 20;
}