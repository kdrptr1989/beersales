using BeerSale.Infrastructure;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Repository.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beer.Commands
{
    public class RemoveBeerCommandHandler : IRequestHandler<RemoveBeerCommand, RemoveBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly IBeerRepository _beerRepository;
        private readonly ILogger<RemoveBeerCommandHandler> _logger;

        public RemoveBeerCommandHandler(
            IBeerSalesDbContext context,
            IBeerRepository beerRepository,
            ILogger<RemoveBeerCommandHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));
            Ensure.ArgumentNotNull(beerRepository, nameof(beerRepository));

            _dbContext = context;
            _logger = logger;
            _beerRepository = beerRepository;
        }

        public async Task<RemoveBeerResponse> Handle(RemoveBeerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(RemoveBeerCommand)} is called");

                var removeAbleBeer = await _dbContext
                    .Beers
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.BreweryId == request.BreweryId);

                if (removeAbleBeer is null)
                {
                    throw new Exception("Beer with Brewery doesn't exists");
                }

                _beerRepository.Delete(removeAbleBeer);
                _beerRepository.Save();

                _logger.Log(LogLevel.Information, $"{nameof(Beer)} entity is removed. Id of the removed entity: {removeAbleBeer.Id} ");

                return new RemoveBeerResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(RemoveBeerCommand)} remove command.");

                return new RemoveBeerResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
                };
            }
        }
    }
}
