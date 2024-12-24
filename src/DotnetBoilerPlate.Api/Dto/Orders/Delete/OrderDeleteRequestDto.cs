using Ardalis.Result;
using MediatR;

namespace DotnetBoilerPlate.Api.Dto.Orders.Delete;

public record OrderDeleteRequestDto : IRequest<Result>
{
    public int Id { get; set; }
}