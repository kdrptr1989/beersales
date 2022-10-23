namespace BeerSales.Api.Models;

public record BeerDto(Guid BreweryId, string Name, decimal Price, decimal AlcoholContent, string Currency)
{
    public BreweryDto Brewery { get; init; }
    public ICollection<StockDto> Stocks { get; init; }
}