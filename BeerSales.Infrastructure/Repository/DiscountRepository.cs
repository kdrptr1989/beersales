using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;

namespace BeerSales.Infrastructure.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(BeerSaleDbContext dbContext) : base(dbContext) { }
    }
}
