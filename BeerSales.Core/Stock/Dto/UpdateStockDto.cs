namespace BeerSales.Core.Stock.Queries.Dto
{
    public record UpdateStockDto(Guid Id, Guid WholesalerId, Guid BeerId, int Quantity);
}
