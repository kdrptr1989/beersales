namespace BeerSales.Core.Beer.Dto;

public record BeerDto(Guid Id, Guid BreweryId, string Name, decimal Price, decimal AlcoholContent, string Currency);
