using BeerSales.Api.Interface;
using BeerSales.Core.Beer.Command;
using MediatR;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class AddBeer : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/AddBeer";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, AddBeerAsync)
                .Produces<AddBeerCommandResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> AddBeerAsync(IMediator mediator, AddBeerCommand command)
        {
            var response = await mediator.Send(command);

            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
