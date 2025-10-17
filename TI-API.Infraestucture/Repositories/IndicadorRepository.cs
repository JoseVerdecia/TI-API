using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class IndicadorRepository : Repository<IndicadorModel>, IIndicadorRepository
    {
        private readonly CommandContext _commandContext;
        private readonly QueryContext _queryContext;
        public IndicadorRepository(CommandContext commandContext, QueryContext queryContext) : base(commandContext, queryContext)
        {
        }

        #region MÉTODOS DE LECTURA (usan QueryContext)

        public async Task<IEnumerable<ObjetivoModel>> GetObjetivosByIndicadorIdAsync(int indicadorId)
        {
            var query = _queryContext.Set<IndicadorDeObjetivoModel>()
                .Where(io => io.IndicadorId == indicadorId)
                .Include(io => io.Objetivo)
                .Select(io => io.Objetivo);

            return await _queryContext.ToListAsync(query);
        }

        public async Task<IEnumerable<IndicadorModel>> GetIndicadoresByEvaluacionAsync(EvaluacionType evaluacion)
        {
            var query = _queryContext.Set<IndicadorModel>()
                .Where(i => i.Evaluacion == evaluacion)
                .Include(i => i.Proceso);

            return await _queryContext.ToListAsync(query);
        }

        public async Task<ProcesoModel?> GetProcesoByIndicadorIdAsync(int indicadorId)
        {
            var indicadorQuery = _queryContext.Set<IndicadorModel>()
                .Where(i => i.Id == indicadorId)
                .Include(i => i.Proceso);

            var indicador = await _queryContext.FirstOrDefaultAsync(indicadorQuery);
            return indicador?.Proceso;
        }

        public async Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByIndicadorIdAsync(int indicadorId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.IndicadorId == indicadorId)
                .Include(ia => ia.Area);

            return await _queryContext.ToListAsync(query);
        }

        public async Task<IndicadorDeAreaModel?> GetIndicadorDeAreaByIdAsync(int indicadorDeAreaId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.Id == indicadorDeAreaId)
                .Include(ia => ia.Indicador)
                .Include(ia => ia.Area);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        public async Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByIndicadorAndAreaAsync(int indicadorId, int areaId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.IndicadorId == indicadorId && ia.AreaId == areaId)
                .Include(ia => ia.Indicador)
                .Include(ia => ia.Area);

            return await _queryContext.ToListAsync(query);
        }

        public async Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByAreaIdAsync(int areaId)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.AreaId == areaId)
                .Include(ia => ia.Indicador)
                .Include(ia => ia.Area);

            return await _queryContext.ToListAsync(query);
        }

        public async Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByAreaAndEvaluacionAsync(int areaId, EvaluacionType evaluacion)
        {
            var query = _queryContext.Set<IndicadorDeAreaModel>()
                .Where(ia => ia.AreaId == areaId && ia.Evaluacion == evaluacion)
                .Include(ia => ia.Indicador)
                .Include(ia => ia.Area);

            return await _queryContext.ToListAsync(query);
        }

        #endregion

        #region MÉTODOS DE ESCRITURA (usan CommandContext)

        public async Task<bool> UpdateEvaluacionAsync(int indicadorId, EvaluacionType evaluacion)
        {
            var indicador = await _commandContext.Indicadores
                .FirstOrDefaultAsync(i => i.Id == indicadorId);

            if (indicador == null) return false;

            indicador.Evaluacion = evaluacion;
            _commandContext.Indicadores.Update(indicador);

            return await _commandContext.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
