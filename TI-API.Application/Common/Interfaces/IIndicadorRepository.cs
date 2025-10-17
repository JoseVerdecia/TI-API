using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IIndicadorRepository : IRepository<IndicadorModel>
    {
        // Métodos específicos para Indicador
        Task<IEnumerable<ObjetivoModel>> GetObjetivosByIndicadorIdAsync(int indicadorId);
        Task<IEnumerable<IndicadorModel>> GetIndicadoresByEvaluacionAsync(EvaluacionType evaluacion);
        Task<ProcesoModel?> GetProcesoByIndicadorIdAsync(int indicadorId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByIndicadorIdAsync(int indicadorId);
        Task<IndicadorDeAreaModel?> GetIndicadorDeAreaByIdAsync(int indicadorDeAreaId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByIndicadorAndAreaAsync(int indicadorId, int areaId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByAreaIdAsync(int areaId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByAreaAndEvaluacionAsync(int areaId, EvaluacionType evaluacion);
    }
}
