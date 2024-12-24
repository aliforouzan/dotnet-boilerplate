namespace DotnetBoilerPlate.Shared.Interfaces;

public interface IPagination
{
    int? CurrentPage { get; set; }
    int? PageSize { get; set; }
}