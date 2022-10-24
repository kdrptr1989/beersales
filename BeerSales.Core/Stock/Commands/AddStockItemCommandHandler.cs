using BeerSale.Infrastructure;
using BeerSales.Core.Beer.Commands;
using BeerSales.Core.Order.Commands.CreateQuote;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Stock.Commands
{
    public class AddStockItemCommandHandler : IRequestHandler<AddStockItemCommand, AddStockItemResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<AddStockItemCommandHandler> _logger;

        public AddStockItemCommandHandler(
            IBeerSalesDbContext context,
            ILogger<AddStockItemCommandHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));

            _dbContext = context;
            _logger = logger;
        }

        public async Task<AddStockItemResponse> Handle(AddStockItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(AddStockItemCommand)} is called");

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

                    _logger.Log(LogLevel.Information, $"{nameof(updatedStockEntity)} entity is updated. Id of the updated entity: {existingStock.Id} ");

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

                    _logger.Log(LogLevel.Information, $"{nameof(Stock)} entity is updated. Id of the created entity: {stockEntity.Id} ");
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new AddStockItemResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(AddStockItemCommand)} updated or created stock item.");

                return new AddStockItemResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
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

