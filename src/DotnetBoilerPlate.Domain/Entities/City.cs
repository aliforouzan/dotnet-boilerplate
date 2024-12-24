using System.Collections.Generic;

namespace DotnetBoilerPlate.Domain.Entities;
public partial class City
{
    public int Id { get; set; }

    public int ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
