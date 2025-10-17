using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class IndicadorDeObjetivoRepository : Repository<IndicadorDeObjetivoModel>, IIndicadorDeObjetivoRepository
    {
        public IndicadorDeObjetivoRepository(CommandContext commandContext, QueryContext queryContext)
            : base(commandContext, queryContext)
        {
        }

        /// <summary>
        /// Obtiene la relación específica entre un indicador y un objetivo (solo lectura)
        /// </summary>
        public async Task<IndicadorDeObjetivoModel?> GetByIndicadorAndObjetivoAsync(int indicadorId, int objetivoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.IndicadorId == indicadorId && io.ObjetivoId == objetivoId);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene todas las relaciones de un indicador con objetivos (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorDeObjetivoModel>> GetByIndicadorIdAsync(int indicadorId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.IndicadorId == indicadorId)
                .Include(io => io.Objetivo);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene todas las relaciones de un objetivo con indicadores (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorDeObjetivoModel>> GetByObjetivoIdAsync(int objetivoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Elimina la relación específica entre un indicador y un objetivo (escritura)
        /// </summary>
        public async Task RemoveByIndicadorAndObjetivoAsync(int indicadorId, int objetivoId)
        {
            var entity = await GetByIndicadorAndObjetivoAsync(indicadorId, objetivoId);
            if (entity != null)
            {
                // Usar el DbSet del CommandContext para eliminación
                var entityToDelete = await _commandContext.Set<IndicadorDeObjetivoModel>()
                    .FirstOrDefaultAsync(io => io.IndicadorId == indicadorId && io.ObjetivoId == objetivoId);

                if (entityToDelete != null)
                {
                    _commandContext.Set<IndicadorDeObjetivoModel>().Remove(entityToDelete);
                }
            }
        }
    }
}
