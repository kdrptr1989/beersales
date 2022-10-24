using BeerSales.Core.Beer.Dto;

namespace BeerSales.Core.Beers.Queries.Dto;

public record BeweryDto(Guid Id, string Name, ICollection<BeerDto> Beers);
