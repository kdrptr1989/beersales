using BeerSale.Infrastructure;
using BeerSales.Core.Beer.Commands;
using BeerSales.Core.Wholesaler.Queries.Dto;
using BeerSales.Core.Wholesaler.Queries.GetAllStocks;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Wholesaler.Queries.GetAllWholesalers
{
    public class GetAllWholesalersQueryHandler : IRequestHandler<GetAllWholesalersQuery, GetAllWholesalersResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<GetAllStocksQueryHandler> _logger;

        public GetAllWholesalersQueryHandler(
            IBeerSalesDbContext context,
            ILogger<GetAllStocksQueryHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));

            _dbContext = context;
            _logger = logger;
        }

        public async Task<GetAllWholesalersResponse> Handle(GetAllWholesalersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(GetAllWholesalersQuery)} is called");

                var listOfWholesalers = await _dbContext
                     .Wholesalers
                     .Select(b => new WholesalerDto(b.Id, b.Name))
                     .PaginatedListAsync(request.PageNumber, request.PageSize);

                return new GetAllWholesalersResponse
                {
                     Success = true,
                     WholesalersList = listOfWholesalers
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(GetAllWholesalersQuery)} request.");

                return new GetAllWholesalersResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
                };
            }
        }
    }
}
