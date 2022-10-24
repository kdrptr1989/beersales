using BeerSale.Infrastructure;
using BeerSales.Core.Beer.Dto;
using BeerSales.Core.Stock.Queries.Dto;
using BeerSales.Core.Wholesaler.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Wholesaler.Queries.GetAllStocks
{
    public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery,GetAllStocksResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<GetAllStocksQueryHandler> _logger;

        public GetAllStocksQueryHandler(
            IBeerSalesDbContext context,
            ILogger<GetAllStocksQueryHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));

            _dbContext = context;
            _logger = logger;
        }

        public async Task<GetAllStocksResponse> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(GetAllStocksQuery)} is called");

                var listOfStocks = await _dbContext
                     .Stocks
                     .Select(b => new StockDto(
                         b.Id, 
                         b.Quantity,
                         new BeerDto(b.Beer.Id,
                                     b.Beer.BreweryId, 
                                     b.Beer.Name, 
                                     b.Beer.Price, 
                                     b.Beer.AlcoholContent,
                                     b.Beer.Currency),
                         new WholesalerDto(b.Wholesaler.Id, b.Wholesaler.Name)))
                     .PaginatedListAsync(request.PageNumber, request.PageSize);

                return new GetAllStocksResponse
                {
                    Success = true,
                    StocksList = listOfStocks
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(GetAllStocksQuery)} request.");

                return new GetAllStocksResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
                };
            }
        }
    }
}
