using BeerSales.Core.Beer.Dto;
using MediatR;

namespace BeerSales.Core.Beer.Commands
{
    public record RemoveBeerCommand(Guid Id, Guid BreweryId) : IRequest<RemoveBeerResponse>;
}
