using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Infrastructure.Data
{
    public class BeerSaleDbContext : DbContext, IBeerSalesDbContext
    {
        public BeerSaleDbContext() { }

        public BeerSaleDbContext(DbContextOptions<BeerSaleDbContext> options) : base(options) { }

        public DbSet<Brewery> Breweries => Set<Brewery>();
        public DbSet<Beer> Beers => Set<Beer>();
        public DbSet<Wholesaler> Wholesalers => Set<Wholesaler>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Discount> Discounts => Set<Discount>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewery>().HasData(Seed.Breweries);
            modelBuilder.Entity<Beer>().HasData(Seed.Beers);
            modelBuilder.Entity<Wholesaler>().HasData(Seed.Wholesalers);
            modelBuilder.Entity<Stock>().HasData(Seed.Stocks);
            modelBuilder.Entity<Discount>().HasData(Seed.Discounts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
