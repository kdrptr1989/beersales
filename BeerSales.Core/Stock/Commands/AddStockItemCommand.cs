using BeerSales.Core.Stock.Queries.Dto;
using MediatR;

namespace BeerSales.Core.Stock.Commands
{
    public record AddStockItemCommand(AddStockItemDto AddStockItem) : IRequest<AddStockItemResponse>;
}
