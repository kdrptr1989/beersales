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
