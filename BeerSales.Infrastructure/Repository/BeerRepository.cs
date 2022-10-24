using BeerSales.Domain.Entities;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;

namespace BeerSales.Infrastructure.Repository
{
    public class BeerRepository : Repository<Beer>, IBeerRepository
    {
        public BeerRepository(BeerSaleDbContext dbContext) : base(dbContext) { }

        public void RemoveBeeById(Guid id)
        {
            var beer = GetAll.SingleOrDefault(s => s.Id == id);

            Delete(beer);
            Save();
        }
    }
}
