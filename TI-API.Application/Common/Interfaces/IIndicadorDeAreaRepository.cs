using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IIndicadorDeAreaRepository : IRepository<IndicadorDeAreaModel>
    {
        // Métodos específicos para IndicadorDeArea
        Task<IEnumerable<IndicadorDeAreaModel>> GetByAreaIdAsync(int areaId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetByAreaAndEvaluacionAsync(int areaId, EvaluacionType evaluacion);
        Task<IndicadorDeAreaModel?> GetByIndicadorAndAreaAsync(int indicadorId, int areaId);
        Task<IEnumerable<IndicadorDeAreaModel>> GetByIndicadorIdAsync(int indicadorId);
    }
}
