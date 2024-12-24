using System;

namespace DotnetBoilerPlate.Api.Dto.Transactions;

public record TransactionResponseDto
{
    public int Id { get; set; }
    
    public string Type { get; set; } = null!;

    public decimal UnitCount { get; set; }

    public decimal PricePerUnit { get; set; }

    public decimal Fee { get; set; }

    public DateTime TradeDate { get; set; }
}