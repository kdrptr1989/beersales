using BeerSales.Core.Beer.Dto;
using BeerSales.Core.Wholesaler.Queries.Dto;

namespace BeerSales.Core.Stock.Queries.Dto
{
    public record StockDto(
        Guid Id, 
        int Quantity, 
        BeerDto BeerDto, 
        WholesalerDto WholesalerDto);
}
