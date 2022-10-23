using BeerSales.Domain.Enumerations;

namespace BeerSales.Domain.Entities;

public record Discount(Guid id, int TierFrom, int TierTo, decimal DiscountValue, DiscountType DiscountType);