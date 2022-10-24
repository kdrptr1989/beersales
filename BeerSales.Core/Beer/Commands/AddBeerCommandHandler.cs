using BeerSale.Infrastructure;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Repository.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Beer.Commands
{
    public class AddBeerCommandHandler : IRequestHandler<AddBeerCommand, AddBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly IBeerRepository _beerRepository;
        private readonly ILogger<RemoveBeerCommandHandler> _logger;

        public AddBeerCommandHandler(
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
                        default);

                _beerRepository.Add(beerEntity);
                _beerRepository.Save();

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
                .Breweries
                .AnyAsync(x => x.Id == request.Beer.BreweryId);

            if (!breweryValidation)
            {
                throw new Exception("Brewery doesn't exist");
            }
        }
    }
}
