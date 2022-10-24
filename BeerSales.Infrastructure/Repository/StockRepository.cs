using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;

namespace BeerSales.Infrastructure.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(BeerSaleDbContext dbContext) : base(dbContext) { }
    }
}
