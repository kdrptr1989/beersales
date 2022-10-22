using BeerSales.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Infrastructure.Data
{
    public class BeerSaleDbContext : DbContext
    {
        public BeerSaleDbContext() { }

        public BeerSaleDbContext(DbContextOptions<BeerSaleDbContext> options) : base(options) { }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Discount> Discounts { get; set; }

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
