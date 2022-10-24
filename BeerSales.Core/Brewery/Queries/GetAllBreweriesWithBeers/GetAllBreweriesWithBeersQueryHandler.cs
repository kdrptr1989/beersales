using BeerSales.Core.Beer.Commands;
using BeerSales.Core.Beer.Dto;
using BeerSales.Core.Beers.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;

public class GetAllBreweriesWithBeersQueryHandler : IRequestHandler<GetAllBreweriesWithBeersQuery, GetAllBreweriesWithBeersResponse>
{
    private readonly IBeerSalesDbContext _dbContext;
    private readonly ILogger<GetAllBreweriesWithBeersQueryHandler> _logger;

    public GetAllBreweriesWithBeersQueryHandler(
            IBeerSalesDbContext context,
            ILogger<GetAllBreweriesWithBeersQueryHandler> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task<GetAllBreweriesWithBeersResponse> Handle(GetAllBreweriesWithBeersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllBreweriesWithBeersQuery)} is called");

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
            _logger.LogError(ex, $"Problem during run {nameof(GetAllBreweriesWithBeersQuery)}.");

            return new GetAllBreweriesWithBeersResponse
            {
                Success = false,
                ErrorMessage = $"Exception message: " + ex.Message
            };
        }                
    }
}
