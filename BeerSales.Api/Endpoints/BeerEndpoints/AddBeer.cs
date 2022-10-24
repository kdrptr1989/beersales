using BeerSales.Api.Interface;
using BeerSales.Core.Beer.Commands;
using FluentValidation;
using MediatR;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class AddBeer : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/AddBeer";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, AddBeerAsync)
                .Produces<AddBeerResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        /// <summary>
        /// Add Beer by a Brewery
        /// </summary>
        /// <param name="command">AddBeer command</param>
        /// <returns></returns>
        private static async Task<IResult> AddBeerAsync(
            IMediator mediator, 
            AddBeerCommand command,
            IValidator<AddBeerCommand> validator,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var response = await mediator.Send(command, cancellationToken);

            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
