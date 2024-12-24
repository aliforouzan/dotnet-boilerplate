using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Shared.Statics;

namespace DotnetBoilerPlate.Application.Filters.Res;

public record PaginatedList<T>
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalItems { get; init; }

    public List<T> Result { get; init; } = new List<T>();

    public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        Result.AddRange(items);
    }

    public PaginatedList()
    {
    }
}

public static class PaginatedListHelper
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,
        int? currentPage = Pagination.DefaultCurrentPage, int? pageSize = Pagination.DefaultPageSize)
    {
        int page = (int) (currentPage is > 0 ? currentPage : Pagination.DefaultCurrentPage);
        int size = (int)(pageSize is > 0 ? pageSize : Pagination.DefaultPageSize);

        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();
        return new PaginatedList<T>(items, count, page, size);
    }
}