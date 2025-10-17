using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TI_API.Application.Common.Interfaces;

namespace TI_API.Infraestucture.Persistence
{
    public class QueryContext : DbContext, IQueryContext
    {
        public QueryContext(DbContextOptions<QueryContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<T> Set<T>() where T : class => base.Set<T>();

        public IQueryable<T> Set<T>(string? includeProperties = null) where T : class
        {
            var query = base.Set<T>().AsNoTracking();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query;
        }

        public async Task<List<T>> ToListAsync<T>(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query)
        {
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> SingleOrDefaultAsync<T>(IQueryable<T> query)
        {
            return await query.SingleOrDefaultAsync();
        }

        public async Task<int> CountAsync<T>(IQueryable<T> query)
        {
            return await query.CountAsync();
        }

        public async Task<bool> AnyAsync<T>(IQueryable<T> query)
        {
            return await query.AnyAsync();
        }

        public async Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            return await query.AnyAsync(predicate);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones optimizadas para consultas
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QueryContext).Assembly);
        }
    }

}
