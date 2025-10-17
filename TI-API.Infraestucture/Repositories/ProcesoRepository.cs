using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class ProcesoRepository : Repository<ProcesoModel>, IProcesoRepository
    {
        public ProcesoRepository(CommandContext commandContext, QueryContext queryContext)
            : base(commandContext, queryContext)
        {
        }

        /// <summary>
        /// Obtiene todos los indicadores de un proceso (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorModel>> GetIndicadoresByProcesoIdAsync(int procesoId)
        {
            var query = _queryContext.Set<IndicadorModel>()
                .Where(i => i.ProcesoId == procesoId)
                .Include(i => i.Proceso);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene un indicador específico de un proceso específico (solo lectura)
        /// </summary>
        public async Task<IndicadorModel?> GetIndicadorByIdAndProcesoIdAsync(int indicadorId, int procesoId)
        {
            var query = _queryContext.Set<IndicadorModel>()
                .Where(i => i.Id == indicadorId && i.ProcesoId == procesoId)
                .Include(i => i.Proceso);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene procesos filtrados por tipo de evaluación (solo lectura)
        /// </summary>
        public async Task<IEnumerable<ProcesoModel>> GetProcesosByEvaluacionAsync(EvaluacionType evaluacion)
        {
            var query = _queryContext.Set<ProcesoModel>()
                .Where(p => p.Evaluacion == evaluacion)
                .Include(p => p.Indicadores);

            return await _queryContext.ToListAsync(query);
        }
    }
}
