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

        private static async Task<IResult> GetAllBreweriesWithBeersAsync(IMediator mediator, int pageNumber, int sizeNumber)
        {
            var response = await mediator.Send(new GetAllBreweriesWithBeersQuery
            {
                PageNumber = pageNumber,
                PageSize = sizeNumber
            });

            return Results.Ok(response);
        }
    }
}
