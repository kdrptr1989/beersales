using BeerSales.Core.Order.Dto;
using MediatR;

namespace BeerSales.Core.Order.Commands.CreateQuote
{
    public record CreateQuoteCommand(ICollection<OrderDto> OrdersList) : IRequest<CreateQuoteResponse>;
}
