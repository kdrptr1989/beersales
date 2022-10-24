using MediatR;

namespace BeerSales.Core.Wholesaler.Queries.GetAllStocks
{
    public record GetAllStocksQuery : BaseQuery, IRequest<GetAllStocksResponse>;
}
