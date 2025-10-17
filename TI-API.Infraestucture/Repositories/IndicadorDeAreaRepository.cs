using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class IndicadorDeAreaRepository : Repository<IndicadorDeAreaModel>, IIndicadorDeAreaRepository
    {
        public IndicadorDeAreaRepository(CommandContext commandContext, QueryContext queryContext)
            : base(commandContext, queryContext)
        {
        }

        /// <summary>
        /// Obtiene todos los indicadores de área de un área específica (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorDeAreaModel>> GetByAreaIdAsync(int areaId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.AreaId == areaId)
                .Include(ia => ia.Area)
                .Include(ia => ia.Indicador);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene indicadores de área de un área filtrados por evaluación (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorDeAreaModel>> GetByAreaAndEvaluacionAsync(int areaId, EvaluacionType evaluacion)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.AreaId == areaId && ia.Evaluacion == evaluacion)
                .Include(ia => ia.Area)
                .Include(ia => ia.Indicador);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene el indicador de área para un indicador y área específicos (solo lectura)
        /// </summary>
        public async Task<IndicadorDeAreaModel?> GetByIndicadorAndAreaAsync(int indicadorId, int areaId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.IndicadorId == indicadorId && ia.AreaId == areaId)
                .Include(ia => ia.Area)
                .Include(ia => ia.Indicador);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene todos los indicadores de área de un indicador específico (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorDeAreaModel>> GetByIndicadorIdAsync(int indicadorId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.IndicadorId == indicadorId)
                .Include(ia => ia.Area)
                .Include(ia => ia.Indicador);

            return await _queryContext.ToListAsync(query);
        }
    }
}
