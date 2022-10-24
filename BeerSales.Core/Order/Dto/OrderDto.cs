namespace BeerSales.Core.Order.Dto
{
    public record OrderDto(Guid WholesalerId, Guid BeerId, int Quantity);
}
