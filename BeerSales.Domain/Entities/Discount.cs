namespace BeerSales.Domain.Entities;

public record Discount(
    Guid id, 
    int TierFrom, 
    decimal DiscountPercentage, 
    DateTime? ModifiedDate) : BaseEntity(ModifiedDate);