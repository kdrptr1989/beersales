namespace BeerSales.Api.Models;

public record BreweryDto(string Name)
{
    public ICollection<BeerDto> Beers { get; init; }
}
