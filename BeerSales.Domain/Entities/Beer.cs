namespace BeerSales.Domain.Entities;

public record Beer(Guid Id, Guid BreweryId, string Name, decimal Price, decimal AlcoholContent, string Currency) 
{
    public Brewery Brewery { get; init; }
    public ICollection<Stock> Stocks { get; init; }
}