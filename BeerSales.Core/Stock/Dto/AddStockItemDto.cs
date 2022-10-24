namespace BeerSales.Core.Stock.Queries.Dto
{
    public record AddStockItemDto(       
        Guid WholesalerId, 
        Guid BeerId, 
        int Quantity);
}
