using BeerSales.Core.Stock.Queries.Dto;
using BeerSales.Infrastructure.Models;

namespace BeerSales.Core.Wholesaler.Queries.GetAllStocks
{
    public record GetAllStocksResponse : BaseResponse
    {
        public PaginatedList<StockDto> StocksList { get; init; }
    }
}
