namespace BeerSales.Api.Models;

public record WholesalerDto(string Name)
{
    public ICollection<StockDto> Stocks { get; init; }
}
