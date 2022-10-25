using BeerSale.Infrastructure;
using BeerSales.Core.Beer.Dto;
using BeerSales.Core.Beers.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using BeerSales.Infrastructure.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;

public class GetAllBreweriesWithBeersQueryHandler : IRequestHandler<GetAllBreweriesWithBeersQuery, GetAllBreweriesWithBeersResponse>
{
    private readonly IBeerSalesDbContext _dbContext;
    private readonly IBreweryRepository _breweryRepository;
    private readonly ILogger<GetAllBreweriesWithBeersQueryHandler> _logger;

    public GetAllBreweriesWithBeersQueryHandler(
            IBeerSalesDbContext context,
            IBreweryRepository breweryRepository,
            ILogger<GetAllBreweriesWithBeersQueryHandler> logger)
    {
        Ensure.ArgumentNotNull(context, nameof(context));
        Ensure.ArgumentNotNull(logger, nameof(logger));
        Ensure.ArgumentNotNull(breweryRepository, nameof(breweryRepository));

        _dbContext = context;
        _logger = logger;
        _breweryRepository = breweryRepository;
    }

    public async Task<GetAllBreweriesWithBeersResponse> Handle(GetAllBreweriesWithBeersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            _logger.Log(LogLevel.Information, $"{nameof(GetAllBreweriesWithBeersQuery)} is called");

            var listOfBreweries = await _breweryRepository
                .GetAll
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
