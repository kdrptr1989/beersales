using MediatR;

namespace BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;

public record GetAllBreweriesWithBeersQuery : BaseQuery, IRequest<GetAllBreweriesWithBeersResponse>;