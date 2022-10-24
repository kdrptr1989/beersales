using BeerSales.Api.Interface;
using BeerSales.Core.Stock.Commands;
using FluentValidation;
using MediatR;

namespace BeerSales.Api.Endpoints.StockEndpoints
{
    public class AddStockItem : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/AddStockItem";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost(Route, AddStockItemAsync)
                .Produces<AddStockItemResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> AddStockItemAsync(
            IMediator mediator,
            AddStockItemCommand command,
            IValidator<AddStockItemCommand> validator,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        }
    }
}
