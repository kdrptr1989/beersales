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

        /// <summary>
        /// Get All Wholesaler with Pagination
        /// </summary>
        /// <param name="pageNumber">Number of Page</param>
        /// <param name="sizeNumber">Number of Size (Page)</param>
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
