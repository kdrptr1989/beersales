namespace BeerSales.Domain.Entities;

public record Brewery(Guid Id, string Name)
{
    public ICollection<Beer> Beers { get; init; }
}