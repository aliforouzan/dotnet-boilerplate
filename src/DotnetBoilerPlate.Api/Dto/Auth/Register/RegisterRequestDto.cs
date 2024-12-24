using System;
using MediatR;
using Ardalis.Result;

namespace DotnetBoilerPlate.Api.Dto.Auth.Register;

public record RegisterRequestDto : IRequest<Result<RegisterResponseDto>>
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string NationalCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }
    
    public string Password { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public int? CityId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }
}
