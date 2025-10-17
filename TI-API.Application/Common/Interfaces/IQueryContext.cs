using System.Linq.Expressions;

namespace TI_API.Application.Common.Interfaces
{
    public interface IQueryContext
    {
        IQueryable<T> Set<T>() where T : class;
        IQueryable<T> Set<T>(string? includeProperties = null) where T : class;
        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query);
        Task<T?> SingleOrDefaultAsync<T>(IQueryable<T> query);
        Task<int> CountAsync<T>(IQueryable<T> query);
        Task<bool> AnyAsync<T>(IQueryable<T> query);
        Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);
    }
}
