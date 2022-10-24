using BeerSale.Infrastructure;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Infrastructure.Repository
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly BeerSaleDbContext _entities;

        public Repository(BeerSaleDbContext dbContext)
        {
            _entities = dbContext;
        }

        #region IRepository Members

        public virtual IQueryable<TEntity> GetAll
        {
            get
            {
                IQueryable<TEntity> query = _entities.Set<TEntity>();
                return query;
            }
        }
        public virtual IQueryable<TEntity> GetAllReadOnly => _entities.Set<TEntity>().AsNoTracking();

        public void Add(TEntity entity)
        {
            Ensure.ArgumentNotNull(entity, nameof(entity));

            _entities.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Ensure.ArgumentNotNull(entity, nameof(entity));

            _entities.Set<TEntity>().Remove(entity);
        }

        public TEntity FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        #endregion
    }
}
