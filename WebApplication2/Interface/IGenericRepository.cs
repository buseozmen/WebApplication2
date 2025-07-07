
namespace WebApplication2.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Update(T entity);
    }

}

