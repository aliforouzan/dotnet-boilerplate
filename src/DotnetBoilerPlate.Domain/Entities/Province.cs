using System.Collections.Generic;

namespace DotnetBoilerPlate.Domain.Entities;
public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
