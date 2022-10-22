namespace BeerSales.Domain.Models;

public record Stock(
    Guid Id, 
    Guid WholesalerId, 
    Guid BeerId, 
    int Quantity)
{
    public Beer Beer { get; init; }
    public Wholesaler Wholesaler { get; init; }
}