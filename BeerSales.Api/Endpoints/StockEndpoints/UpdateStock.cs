using BeerSales.Api.Interface;
using BeerSales.Core.Stock.Commands;
using MediatR;

namespace BeerSales.Api.Endpoints.StockEndpoints
{
    public class UpdateStock : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/UpdateStock";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, UpdateStockAsync)
                .Produces<UpdateStockResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> UpdateStockAsync(
            IMediator mediator,
            UpdateStockCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        }
    }
}
