using BeerSales.Core.Beers.Queries.Dto;
using BeerSales.Infrastructure.Models;

namespace BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;

public record GetAllBreweriesWithBeersResponse : BaseResponse
{
    public PaginatedList<BeweryDto> BeweriesList { get; init; }
}
