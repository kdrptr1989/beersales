using BeerSales.Core.Stock.Queries.Dto;
using MediatR;

namespace BeerSales.Core.Stock.Commands
{
    public record UpdateStockCommand(UpdateStockDto UpdateStockDto) : IRequest<UpdateStockResponse>;
}
