using TI_API.Domain.Entities;

namespace TI_API.Application.Common.Interfaces
{
    public interface IIndicadorDeObjetivoRepository : IRepository<IndicadorDeObjetivoModel>
    {
        Task<IndicadorDeObjetivoModel?> GetByIndicadorAndObjetivoAsync(int indicadorId, int objetivoId);
        Task<IEnumerable<IndicadorDeObjetivoModel>> GetByIndicadorIdAsync(int indicadorId);
        Task<IEnumerable<IndicadorDeObjetivoModel>> GetByObjetivoIdAsync(int objetivoId);
        Task RemoveByIndicadorAndObjetivoAsync(int indicadorId, int objetivoId);
    }
}
