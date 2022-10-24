using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;

namespace BeerSales.Infrastructure.Repository
{
    public class WholesalerRepository : Repository<Wholesaler>, IWholesalerRepository
    {
        public WholesalerRepository(BeerSaleDbContext dbContext) : base(dbContext) { }
    }
}
