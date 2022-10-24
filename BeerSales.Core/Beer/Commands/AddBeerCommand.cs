using BeerSales.Core.Beer.Dto;
using MediatR;

namespace BeerSales.Core.Beer.Commands
{
    public record AddBeerCommand(BeerDto Beer) : IRequest<AddBeerResponse>;
}
