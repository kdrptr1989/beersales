using BeerSale.Infrastructure;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Stock.Commands
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, UpdateStockResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<UpdateStockCommandHandler> _logger;

        public UpdateStockCommandHandler(
            IBeerSalesDbContext context,
            ILogger<UpdateStockCommandHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));

            _dbContext = context;
            _logger = logger;
        }

        public async Task<UpdateStockResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(UpdateStockCommand)} is called");

                await ValidateRequest(request, cancellationToken);

                var stockEntity = new Domain.Entities.Stock(
                    request.UpdateStockDto.Id,
                    request.UpdateStockDto.WholesalerId,
                    request.UpdateStockDto.BeerId,
                    request.UpdateStockDto.Quantity,
                    DateTime.UtcNow);

                _dbContext.Stocks.Update(stockEntity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.Log(LogLevel.Information, $"Updated {nameof(Stock)} entity is success. Id of the updated entity: {stockEntity.Id} ");

                return new UpdateStockResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(UpdateStockCommand)} update.");

                return new UpdateStockResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
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


