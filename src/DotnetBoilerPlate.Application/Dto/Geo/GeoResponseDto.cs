namespace DotnetBoilerPlate.Application.Dto.Geo;

public record GeoResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}