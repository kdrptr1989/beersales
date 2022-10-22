using BeerSales.Domain.Enumerations;

namespace BeerSales.Domain.Models;

public record Discount(
    Guid Id,
    int TierFrom, 
    int TierTo, 
    decimal DiscountValue, 
    DiscountType DiscountType);