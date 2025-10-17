using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TI_API.Application.Common.Interfaces;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CommandContext _commandContext;
        protected readonly QueryContext _queryContext;
        protected readonly DbSet<T> _commandDbSet;

        public Repository(CommandContext commandContext, QueryContext queryContext)
        {
            _commandContext = commandContext;
            _queryContext = queryContext;
            _commandDbSet = _commandContext.Set<T>();
        }

        #region MÉTODOS DE LECTURA (usan QueryContext)

        /// <summary>
        /// Obtiene una entidad por su ID numérico (solo lectura)
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            // Usar el DbSet del CommandContext para FindAsync (no disponible en IQueryable)
            return await _commandContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene una entidad por su ID GUID (solo lectura)
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            // Usar el DbSet del CommandContext para FindAsync (no disponible en IQueryable)
            return await _commandContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene todas las entidades (solo lectura)
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = _queryContext.Set<T>();
            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Busca entidades que cumplen una condición (solo lectura)
        /// </summary>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _queryContext.Set<T>().Where(predicate);
            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene la primera entidad que cumple una condición con relaciones incluidas (solo lectura)
        /// </summary>
        public virtual async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            var query = _queryContext.Set<T>(includeProperties).Where(predicate);
            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene todas las entidades que cumplen una condición con relaciones incluidas (solo lectura)
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            var query = _queryContext.Set<T>(includeProperties).Where(predicate);
            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene todas las entidades con relaciones incluidas (solo lectura)
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
        {
            var query = _queryContext.Set<T>(includeProperties);
            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Cuenta todas las entidades (solo lectura)
        /// </summary>
        public virtual async Task<int> CountAsync()
        {
            var query = _queryContext.Set<T>();
            return await _queryContext.CountAsync(query);
        }

        /// <summary>
        /// Cuenta entidades que cumplen una condición (solo lectura)
        /// </summary>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _queryContext.Set<T>().Where(predicate);
            return await _queryContext.CountAsync(query);
        }

        #endregion

        #region MÉTODOS DE ESCRITURA (usan CommandContext)

        /// <summary>
        /// Agrega una entidad al contexto (escritura)
        /// </summary>
        public virtual void Add(T entity)
        {
            _commandDbSet.Add(entity);
        }

        /// <summary>
        /// Agrega una entidad de forma asíncrona (escritura)
        /// </summary>
        public virtual async Task AddAsync(T entity)
        {
            await _commandDbSet.AddAsync(entity);
        }

        /// <summary>
        /// Agrega múltiples entidades (escritura)
        /// </summary>
        public virtual void AddRange(IEnumerable<T> entities)
        {
            _commandDbSet.AddRange(entities);
        }

        /// <summary>
        /// Agrega múltiples entidades de forma asíncrona (escritura)
        /// </summary>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _commandDbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Elimina una entidad (escritura)
        /// </summary>
        public virtual void Remove(T entity)
        {
            _commandDbSet.Remove(entity);
        }

        /// <summary>
        /// Elimina múltiples entidades (escritura)
        /// </summary>
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _commandDbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Actualiza una entidad (escritura)
        /// </summary>
        public virtual void Update(T entity)
        {
            _commandDbSet.Update(entity);
        }

        #endregion

        #region MÉTODOS ADICIONALES PARA CQRS

        /// <summary>
        /// Obtiene una consulta IQueryable para consultas complejas (solo lectura)
        /// </summary>
        protected virtual IQueryable<T> GetQueryable(string? includeProperties = null)
        {
            return _queryContext.Set<T>(includeProperties);
        }

        /// <summary>
        /// Obtiene una consulta con filtros adicionales (solo lectura)
        /// </summary>
        protected virtual IQueryable<T> GetQueryableWithFilter(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            return _queryContext.Set<T>(includeProperties).Where(predicate);
        }

        /// <summary>
        /// Ejecuta una consulta SQL personalizada (solo lectura)
        /// </summary>
        public virtual async Task<IEnumerable<T>> FromSqlRawAsync(string sql, params object[] parameters)
        {
            // Usar el DbSet del CommandContext para FromSqlRaw (no disponible en IQueryable)
            return await _commandContext.Set<T>()
                .FromSqlRaw(sql, parameters)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Verifica si existe una entidad (solo lectura)
        /// </summary>
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _queryContext.Set<T>().Where(predicate);
            return await _queryContext.AnyAsync(query);
        }

        #endregion
    }
}
