using DotnetBoilerPlate.Application.Dto.Transactions;
using System;
using System.Collections.Generic;

namespace DotnetBoilerPlate.Application.Dto.Orders;

public record OrderResponseDto
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }

    public string Type { get; set; } = null!;

    public decimal UnitCount { get; set; }
    
    public decimal DoneCount { get; set; }

    public decimal PricePerUnit { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public virtual ICollection<TransactionResponseDto> Transactions { get; set; } = new List<TransactionResponseDto>();
}