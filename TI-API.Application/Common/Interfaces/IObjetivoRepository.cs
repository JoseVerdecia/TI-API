using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IObjetivoRepository : IRepository<ObjetivoModel>
    {
        // Métodos específicos para Objetivo
        Task<IEnumerable<IndicadorModel>> GetIndicadoresByProcesoAndObjetivoAsync(int procesoId, int objetivoId);
        Task<IEnumerable<IndicadorModel>> GetIndicadoresByObjetivoIdAsync(int objetivoId);
        Task<IEnumerable<ObjetivoModel>> GetObjetivosByEvaluacionAsync(EvaluacionType evaluacion);
        Task<IEnumerable<ProcesoModel>> GetProcesosByObjetivoIdAsync(int objetivoId);
        Task<ProcesoModel?> GetProcesoByObjetivoAndProcesoIdAsync(int objetivoId, int procesoId);
        Task<IndicadorModel?> GetIndicadorByObjetivoAndIndicadorIdAsync(int objetivoId, int indicadorId);
    }
}
