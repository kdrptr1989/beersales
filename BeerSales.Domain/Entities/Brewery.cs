namespace BeerSales.Domain.Entities;

public record Brewery(Guid Id, string Name, DateTime? ModifiedDate) : BaseEntity(ModifiedDate)
{
    public ICollection<Beer> Beers { get; init; }
}