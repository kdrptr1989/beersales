using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beer.Commands
{
    public class RemoveBeerCommandHandler : IRequestHandler<RemoveBeerCommand, RemoveBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<RemoveBeerCommandHandler> _logger;

        public RemoveBeerCommandHandler(
            IBeerSalesDbContext context,
            ILogger<RemoveBeerCommandHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<RemoveBeerResponse> Handle(RemoveBeerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"{nameof(RemoveBeerCommand)} is called");

                var removeAbleBeer = await _dbContext
                    .Beers
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.BreweryId == request.BreweryId);

                if (removeAbleBeer is null)
                {
                    throw new Exception("Beer with Brewery doesn't exists");
                }

                _dbContext.Beers.Remove(removeAbleBeer);
                await _dbContext.SaveChangesAsync(cancellationToken);

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
