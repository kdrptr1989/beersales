namespace BeerSales.Core;

public record BaseQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
