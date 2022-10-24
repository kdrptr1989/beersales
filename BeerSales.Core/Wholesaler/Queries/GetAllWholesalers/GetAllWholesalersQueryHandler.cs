using BeerSales.Core.Wholesaler.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Wholesaler.Queries.GetAllWholesalers
{
    public class GetAllWholesalersQueryHandler : IRequestHandler<GetAllWholesalersQuery, GetAllWholesalersResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly ILogger<GetAllWholesalersQueryHandler> _logger;

        public GetAllWholesalersQueryHandler(
            IBeerSalesDbContext context,
            ILogger<GetAllWholesalersQueryHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<GetAllWholesalersResponse> Handle(GetAllWholesalersQuery request, CancellationToken cancellationToken)
        {
            try
            {
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
