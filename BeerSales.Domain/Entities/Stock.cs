namespace BeerSales.Domain.Entities;

public record Stock(
    Guid Id,
    Guid WholesalerId, 
    Guid BeerId, 
    int Quantity, 
    DateTime? ModifiedDate) : BaseEntity(ModifiedDate)
{
    public Beer Beer { get; init; }
    public Wholesaler Wholesaler { get; init; }
}