using BeerSales.Infrastructure.Interfaces;
using MediatR;

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
                var stockEntity = new Domain.Entities.Stock(
                   request.UpdateStockDto.Id,
                    request.UpdateStockDto.WholesalerId,
                    request.UpdateStockDto.BeerId,
                    request.UpdateStockDto.Quantity);

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
    }
}

