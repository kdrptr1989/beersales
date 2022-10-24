namespace BeerSales.Core.Order.Dto
{
    public record OrderSummaryDto(
        Guid WholesalerId, 
        string WholesalerName,
        Guid BeerId, 
        string BeerName,
        int Quantity, 
        decimal Price,
        string Currency,
        decimal SubTotalPrice);
}
