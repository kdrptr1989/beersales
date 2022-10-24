using BeerSales.Core.Beer.Dto;
using BeerSales.Core.Beers.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;

namespace BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;

public class GetAllBreweriesWithBeersQueryHandler : IRequestHandler<GetAllBreweriesWithBeersQuery, GetAllBreweriesWithBeersResponse>
{
    private readonly IBeerSalesDbContext _dbContext;

    public GetAllBreweriesWithBeersQueryHandler(IBeerSalesDbContext context)
    {
        _dbContext = context;
    }

    public async Task<GetAllBreweriesWithBeersResponse> Handle(GetAllBreweriesWithBeersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var listOfBreweries = await _dbContext
                 .Breweries
                 .Select(b => new BeweryDto(
                        b.Id,
                        b.Name,
                        b.Beers.Select(c => new BeerDto(c.Id, c.BreweryId, c.Name, c.Price, c.AlcoholContent, c.Currency)).ToList()))
                 .PaginatedListAsync(request.PageNumber, request.PageSize);

            return new GetAllBreweriesWithBeersResponse
            {
                BeweriesList = listOfBreweries,
                Success = true
            };
        }
        catch(Exception ex)
        {
            // Todo logger and validation
            return new GetAllBreweriesWithBeersResponse
            {
                Success = false,
                ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
            };
        }                
    }
}
