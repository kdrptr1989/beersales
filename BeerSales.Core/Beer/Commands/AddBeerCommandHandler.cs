using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beer.Commands
{
    public class AddBeerCommandHandler : IRequestHandler<AddBeerCommand, AddBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<AddBeerCommandHandler> _logger;

        public AddBeerCommandHandler(
            IBeerSalesDbContext context,
            ILogger<AddBeerCommandHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<AddBeerResponse> Handle(AddBeerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"{nameof(AddBeerCommand)} is called");

                await ValidateRequest(request, cancellationToken);

                var beerEntity = new Domain.Entities.Beer(
                        Guid.NewGuid(),
                        request.Beer.BreweryId,
                        request.Beer.Name,
                        request.Beer.Price,
                        request.Beer.AlcoholContent,
                        request.Beer.Currency,
                        DateTime.UtcNow);

                _dbContext.Beers.Add(beerEntity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.Log(LogLevel.Information, $"New {nameof(Beer)} entity is saved. Id of the new entity: {beerEntity.Id} ");

                return new AddBeerResponse
                {
                    Success = true,
                    Id = beerEntity.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(AddBeerCommand)} saving.");             

                return new AddBeerResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
                };
            }
        }

        private async Task ValidateRequest(AddBeerCommand request, CancellationToken cancellationToken)
        {
            var breweryValidation = await _dbContext
                .Stocks
                .AnyAsync(x => x.Id == request.Beer.BreweryId);

            if (!breweryValidation)
            {
                throw new Exception("Brewery doesn't exist");
            }
        }
    }
}
