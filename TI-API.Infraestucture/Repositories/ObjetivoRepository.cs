using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class ObjetivoRepository : Repository<ObjetivoModel>, IObjetivoRepository
    {
        public ObjetivoRepository(CommandContext commandContext, QueryContext queryContext)
            : base(commandContext, queryContext)
        {
        }

        /// <summary>
        /// Obtiene indicadores de un proceso específico que pertenecen a un objetivo específico (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorModel>> GetIndicadoresByProcesoAndObjetivoAsync(int procesoId, int objetivoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso)
                .Where(io => io.Indicador.ProcesoId == procesoId)
                .Select(io => io.Indicador);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene todos los indicadores asociados a un objetivo (solo lectura)
        /// </summary>
        public async Task<IEnumerable<IndicadorModel>> GetIndicadoresByObjetivoIdAsync(int objetivoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso)
                .Select(io => io.Indicador);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene objetivos filtrados por tipo de evaluación (solo lectura)
        /// </summary>
        public async Task<IEnumerable<ObjetivoModel>> GetObjetivosByEvaluacionAsync(EvaluacionType evaluacion)
        {
            var query = _queryContext.Set<ObjetivoModel>()
                .Where(o => o.Evaluacion == evaluacion)
                .Include(o => o.IndicadoresAsignados)
                    .ThenInclude(io => io.Indicador);

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene todos los procesos que tienen indicadores asociados a un objetivo (solo lectura)
        /// </summary>
        public async Task<IEnumerable<ProcesoModel>> GetProcesosByObjetivoIdAsync(int objetivoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso)
                .Select(io => io.Indicador.Proceso)
                .Distinct();

            return await _queryContext.ToListAsync(query);
        }

        /// <summary>
        /// Obtiene un proceso específico que tiene indicadores asociados a un objetivo (solo lectura)
        /// </summary>
        public async Task<ProcesoModel?> GetProcesoByObjetivoAndProcesoIdAsync(int objetivoId, int procesoId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso)
                .Where(io => io.Indicador.ProcesoId == procesoId)
                .Select(io => io.Indicador.Proceso);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene un indicador específico asociado a un objetivo (solo lectura)
        /// </summary>
        public async Task<IndicadorModel?> GetIndicadorByObjetivoAndIndicadorIdAsync(int objetivoId, int indicadorId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.ObjetivoId == objetivoId && io.IndicadorId == indicadorId)
                .Include(io => io.Indicador)
                    .ThenInclude(i => i.Proceso)
                .Select(io => io.Indicador);

            return await _queryContext.FirstOrDefaultAsync(query);
        }
    }
}
