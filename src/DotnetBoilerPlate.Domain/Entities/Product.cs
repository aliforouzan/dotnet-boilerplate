using System.Collections.Generic;

namespace DotnetBoilerPlate.Domain.Entities;
public partial class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public decimal SellFee { get; set; }
    public decimal BuyFee { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
