﻿namespace BeerSales.Domain.Models;

public record Wholesaler(Guid Id, string Name)
{
    public ICollection<Stock> Stocks { get; init; }    
}