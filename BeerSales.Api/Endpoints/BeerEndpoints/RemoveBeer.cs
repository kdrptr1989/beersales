using BeerSales.Api.Interface;
using BeerSales.Core.Beer.Commands;
using MediatR;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class RemoveBeer : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/RemoveBeer";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, RemoveBeerAsync)
                .Produces<RemoveBeerResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        /// <summary>
        /// Remove Beer by a Brewery
        /// </summary>
        /// <param name="command">RemoveBeer command</param>
        private static async Task<IResult> RemoveBeerAsync(
            IMediator mediator, 
            RemoveBeerCommand command,
            CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);

            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
