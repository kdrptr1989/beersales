using BeerSales.Api.Interface;
using BeerSales.Core.Wholesaler.Queries.GetAllWholesalers;
using MediatR;

namespace BeerSales.Api.Endpoints.WholesalerEndpoints
{
    public class GetAllWholesalers : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/GetAllWholesalers";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet(Route, GetAllWholesalersAsync)
                .Produces<GetAllWholesalersResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> GetAllWholesalersAsync(
            IMediator mediator,
            int pageNumber,
            int sizeNumber,
            CancellationToken cancellationToken)
        {
            var command = new GetAllWholesalersQuery() with
            {
                PageNumber = pageNumber,
                PageSize = sizeNumber
            };

            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        }
    }
}
