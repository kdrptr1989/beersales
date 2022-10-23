namespace BeerSales.Api.Models;

public record StockDto(Guid WholesalerId, Guid BeerId, int Quantity)
{
    public BeerDto Beer { get; init; }
    public WholesalerDto Wholesaler { get; init; }
}