using TI_API.Domain.Enums;
using TI_API.Entities;

namespace TI_API.Application.Common.Interfaces
{
    public interface IAreaRepository : IRepository<AreaModel>
    {

        Task<AreaModel?> GetAreaWithJefeAsync(int areaId);
        Task<IEnumerable<AreaModel>> GetAreasByTipoAsync(AreaType tipo);
    }
}
