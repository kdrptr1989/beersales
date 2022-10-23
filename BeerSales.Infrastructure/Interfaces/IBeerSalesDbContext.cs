using BeerSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Infrastructure.Interfaces
{
    public interface IBeerSalesDbContext
    {
        DbSet<Brewery> Breweries { get; }
        DbSet<Beer> Beers { get; }
        DbSet<Wholesaler> Wholesalers { get; }
        DbSet<Stock> Stocks { get; }
        DbSet<Discount> Discounts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
