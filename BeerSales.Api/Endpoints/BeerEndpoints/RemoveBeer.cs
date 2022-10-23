using BeerSales.Api.Interface;
using BeerSales.Core.Beer.Command;
using MediatR;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class RemoveBeer : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/RemoveBeer";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, RemoveBeerAsync)
                .Produces<RemoveBeerCommandResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> RemoveBeerAsync(IMediator mediator, RemoveBeerCommand command)
        {
            var response = await mediator.Send(command);

            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
