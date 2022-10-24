using BeerSales.Api.Interface;
using BeerSales.Core.Order.Commands.CreateOrder;
using MediatR;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class CreateQuote : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/CreateQuote";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, CreateQuoteAsync)
                .Produces<CreateQuoteResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> CreateQuoteAsync(IMediator mediator, CreateQuoteCommand command)
        {
            var response = await mediator.Send(command);

            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
