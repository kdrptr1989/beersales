using BeerSales.Core.Wholesaler.Queries.Dto;
using BeerSales.Infrastructure.Models;

namespace BeerSales.Core.Wholesaler.Queries.GetAllWholesalers;

public record GetAllWholesalersResponse : BaseResponse
{
    public PaginatedList<WholesalerDto> WholesalersList { get; init; }
}
