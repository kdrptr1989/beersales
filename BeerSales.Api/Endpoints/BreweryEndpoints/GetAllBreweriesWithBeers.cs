using BeerSales.Api.Interface;
using BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;
using BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers.Dto;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BeerSales.Api.Endpoints.BeerEndpoints
{
    public class GetAllBreweriesWithBeers : IEndpoint
    {
        public const string Route = $"{EndpointConstant.BaseRoute}/getAllBeersByBeweries";

        public static void DefineEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet(Route, GetAllByBreweriesAsync)
                .Produces<List<BeweriesDto>>()
                .WithTags(EndpointConstant.Tag);
        }

        private static async Task<IResult> GetAllByBreweriesAsync(IMediator mediator, int pageNumber, int sizeNumber)
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
