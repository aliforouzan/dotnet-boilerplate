using DotnetBoilerPlate.Domain.Entities.Enums;
using DotnetBoilerPlate.Shared.Types;
using System;
using System.Collections.Generic;

namespace DotnetBoilerPlate.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public UserId UserId { get; set; }

    public int ProductId { get; set; }

    public string Type { get; set; } = null!;

    public decimal UnitCount { get; set; }

    public decimal DoneCount { get; set; }

    public decimal PricePerUnit { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public virtual User User { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public Order(UserId userId, int productId, string type, decimal unitCount, decimal pricePerUnit)
    {
        UserId = userId;
        ProductId = productId;
        Type = type;
        UnitCount = unitCount;
        DoneCount = 0;
        PricePerUnit = pricePerUnit;
        Status = OrderStatus.Pending.ToString();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public bool IsProcessable(Product product)
    {
        return true;
    }

    public bool IsDeletable()
    {
        return Status == OrderStatus.Pending.ToString() || Status == OrderStatus.PartialSuccess.ToString();
    }
    
    public bool IsEditable()
    {
        return Status == OrderStatus.Pending.ToString();
    }
}