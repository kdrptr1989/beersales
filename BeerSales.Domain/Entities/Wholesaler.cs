namespace BeerSales.Domain.Entities;

public record Wholesaler(
    Guid Id, 
    string Name, 
    DateTime? ModifiedDate) : BaseEntity(ModifiedDate)
{
    public ICollection<Stock> Stocks { get; init; }    
}