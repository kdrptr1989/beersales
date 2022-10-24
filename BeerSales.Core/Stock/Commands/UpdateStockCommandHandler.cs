using BeerSales.Core.Order.Commands.CreateOrder;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Core.Stock.Commands
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, UpdateStockResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public UpdateStockCommandHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UpdateStockResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateRequest(request, cancellationToken);

                var stockEntity = new Domain.Entities.Stock(
                    request.UpdateStockDto.Id,
                    request.UpdateStockDto.WholesalerId,
                    request.UpdateStockDto.BeerId,
                    request.UpdateStockDto.Quantity,
                    DateTime.UtcNow);

                _dbContext.Stocks.Update(stockEntity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new UpdateStockResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // TODO logging
                return new UpdateStockResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
                };
            }
        }

        private async Task ValidateRequest(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var stockValidation = await _dbContext
                .Stocks
                .AnyAsync(x => x.Id == request.UpdateStockDto.Id);

            if (!stockValidation)
            {
                throw new Exception("Stock doesn't exist");
            }

            var beerValidation = await _dbContext
               .Beers
               .AnyAsync(x => x.Id == request.UpdateStockDto.BeerId);

            if (!beerValidation)
            {
                throw new Exception("Beer doesn't exist");
            }

            var wholeSalerValidation = await _dbContext
              .Beers
              .AnyAsync(x => x.Id == request.UpdateStockDto.WholesalerId);

            if (!wholeSalerValidation)
            {
                throw new Exception("Wholesaler doesn't exist");
            }
        }
    }
}


