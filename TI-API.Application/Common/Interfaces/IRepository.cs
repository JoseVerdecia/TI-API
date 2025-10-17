using System.Linq.Expressions;

namespace TI_API.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Métodos básicos
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Métodos con includes
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);
        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);

        // Operaciones CRUD
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);

        // Métodos de conteo
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
