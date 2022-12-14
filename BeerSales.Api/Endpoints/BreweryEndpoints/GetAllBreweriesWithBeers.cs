using BeerSales.Api.Interface;
using BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;
using MediatR;

namespace BeerSales.Api.Endpoints.BreweryEndpoints
{
    public class GetAllBreweriesWithBeers : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/GetAllBreweriesWithBeers";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet(Route, GetAllBreweriesWithBeersAsync)
                .Produces<GetAllBreweriesWithBeersResponse>()
                .WithTags(EndpointConstant.Tag);
        }

        /// <summary>
        /// Get All Breweries with Beers. Pagination supported.
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="sizeNumber">Number of Size (Page)</param>
        private static async Task<IResult> GetAllBreweriesWithBeersAsync(
            IMediator mediator, 
            int pageNumber, 
            int sizeNumber, 
            CancellationToken cancellationToken)
        {
            var command = new GetAllBreweriesWithBeersQuery() with
            {
                PageNumber = pageNumber,
                PageSize = sizeNumber
            };   

            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        }
    }
}
