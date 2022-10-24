using BeerSales.Core.Stock.Commands;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Core.Beer.Commands
{
    public class AddBeerCommandHandler : IRequestHandler<AddBeerCommand, AddBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public AddBeerCommandHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<AddBeerResponse> Handle(AddBeerCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                return new AddBeerResponse
                {
                    Success = true,
                    Id = beerEntity.Id
                };
            }
            catch (Exception ex)
            {
                // TODO logging
                return new AddBeerResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
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
