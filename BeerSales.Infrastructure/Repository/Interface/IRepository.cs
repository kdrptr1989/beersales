namespace BeerSales.Infrastructure.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }
        IQueryable<T> GetAllReadOnly { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int id);
        void Save();
    }
}
