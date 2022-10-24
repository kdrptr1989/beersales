using BeerSales.Core.Wholesaler.Queries.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Mappings;
using MediatR;

namespace BeerSales.Core.Wholesaler.Queries.GetAllWholesalers
{
    public class GetAllWholesalersQueryHandler : IRequestHandler<GetAllWholesalersQuery, GetAllWholesalersResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public GetAllWholesalersQueryHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<GetAllWholesalersResponse> Handle(GetAllWholesalersQuery request, CancellationToken cancellationToken)
        {
            try
            {
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
                return new GetAllWholesalersResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
                };
            }
        }
    }
}
