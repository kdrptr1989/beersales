using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Core.Stock.Commands
{
    public class AddStockItemCommandHandler : IRequestHandler<AddStockItemCommand, AddStockItemResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public AddStockItemCommandHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<AddStockItemResponse> Handle(AddStockItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateRequest(request, cancellationToken);

                var existingStock = _dbContext
                    .Stocks
                    .FirstOrDefault(x => x.WholesalerId == request.AddStockItem.WholesalerId && x.BeerId == request.AddStockItem.BeerId);

                if (existingStock != null)
                {
                    var updatedStockEntity = new Domain.Entities.Stock(
                       existingStock.Id,
                       existingStock.WholesalerId,
                       existingStock.BeerId,
                       existingStock.Quantity + request.AddStockItem.Quantity,
                       DateTime.UtcNow);

                    _dbContext.Stocks.Update(updatedStockEntity);
                }
                else
                {
                    var stockEntity = new Domain.Entities.Stock(
                        Guid.NewGuid(),
                        request.AddStockItem.WholesalerId,
                        request.AddStockItem.BeerId,
                        request.AddStockItem.Quantity,
                        default);

                    _dbContext.Stocks.Add(stockEntity);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new AddStockItemResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // TODO logging
                return new AddStockItemResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
                };
            }
        }

        private async Task ValidateRequest(AddStockItemCommand request, CancellationToken cancellationToken)
        {          
            var beerValidation = await _dbContext
               .Beers
               .AnyAsync(x => x.Id == request.AddStockItem.BeerId);

            if (!beerValidation)
            {
                throw new Exception("Beer doesn't exist");
            }

            var wholeSalerValidation = await _dbContext
              .Beers
              .AnyAsync(x => x.Id == request.AddStockItem.WholesalerId);

            if (!wholeSalerValidation)
            {
                throw new Exception("Wholesaler doesn't exist");
            }
        }
    }
}

