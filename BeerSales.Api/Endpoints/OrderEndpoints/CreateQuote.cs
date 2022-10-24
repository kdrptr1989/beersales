using BeerSales.Api.Interface;
using BeerSales.Core.Order.Commands.CreateQuote;
using FluentValidation;
using MediatR;

namespace BeerSales.Api.Endpoints.OrderEndpoints
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

        /// <summary>
        /// Create Quote by the Client
        /// </summary>
        /// <param name="command">Create Quote command</param>
        /// <returns></returns>
        private static async Task<IResult> CreateQuoteAsync(
            IMediator mediator, 
            CreateQuoteCommand command,
            IValidator<CreateQuoteCommand> validator,
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
