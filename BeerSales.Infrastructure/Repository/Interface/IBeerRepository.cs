using BeerSales.Domain.Entities;

namespace BeerSales.Infrastructure.Repository.Interface
{
    public interface IBeerRepository : IRepository<Beer>
    {
        void RemoveBeeById(Guid id);
    }
}
