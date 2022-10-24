using BeerSales.Api.Interface;
using BeerSales.Core.Wholesaler.Queries.GetAllStocks;
using MediatR;

namespace BeerSales.Api.Endpoints.StockEndpoints
{
    public class GetAllStocks : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/GetAllStocks";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet(Route, GetAllStocksAsync)
                .Produces<GetAllStocksResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        /// <summary>
        /// Get All Stock with Pagination
        /// </summary>
        /// <param name="pageNumber">Number of Page</param>
        /// <param name="sizeNumber">Number of Size (Page)</param>
        private static async Task<IResult> GetAllStocksAsync(
            IMediator mediator,
            int pageNumber,
            int sizeNumber,
            CancellationToken cancellationToken)
        {
            var command = new GetAllStocksQuery() with
            {
                PageNumber = pageNumber,
                PageSize = sizeNumber
            };

            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        }
    }
}
