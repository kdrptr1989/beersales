using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;

namespace BeerSales.Infrastructure.Repository
{
    public class BreweryRepository : Repository<Brewery>, IBreweryRepository
    {
        public BreweryRepository(BeerSaleDbContext dbContext) : base(dbContext) { }
    }
}
