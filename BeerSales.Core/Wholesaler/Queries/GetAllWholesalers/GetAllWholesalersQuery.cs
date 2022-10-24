using MediatR;

namespace BeerSales.Core.Wholesaler.Queries.GetAllWholesalers;

public record GetAllWholesalersQuery : BaseQuery, IRequest<GetAllWholesalersResponse>;
