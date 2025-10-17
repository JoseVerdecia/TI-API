using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IProcesoRepository : IRepository<ProcesoModel>
    {
        // Métodos específicos para Proceso
        Task<IEnumerable<IndicadorModel>> GetIndicadoresByProcesoIdAsync(int procesoId);
        Task<IndicadorModel?> GetIndicadorByIdAndProcesoIdAsync(int indicadorId, int procesoId);
        Task<IEnumerable<ProcesoModel>> GetProcesosByEvaluacionAsync(EvaluacionType evaluacion);
    }
}
