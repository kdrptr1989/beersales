using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Core.Beer.Commands
{
    public class RemoveBeerCommandHandler : IRequestHandler<RemoveBeerCommand, RemoveBeerResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public RemoveBeerCommandHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<RemoveBeerResponse> Handle(RemoveBeerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var removeAbleBeer = await _dbContext
                    .Beers
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.BreweryId == request.BreweryId);

                if (removeAbleBeer is null)
                {
                    throw new Exception("Beer with Brewery doesn't exists");
                }

                _dbContext.Beers.Remove(removeAbleBeer);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new RemoveBeerResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // TODO logging
                return new RemoveBeerResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
                };
            }
        }
    }
}
