using Microsoft.EntityFrameworkCore;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Enums;
using TI_API.Entities;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class AreaRepository : Repository<AreaModel>, IAreaRepository
    {
        public AreaRepository(CommandContext commandContext, QueryContext queryContext)
            : base(commandContext, queryContext)
        {
        }

        /// <summary>
        /// Obtiene un área con su jefe asignado incluido (solo lectura)
        /// </summary>
        public async Task<AreaModel?> GetAreaWithJefeAsync(int areaId)
        {
            var query = _queryContext.Set<AreaModel>()
                .Where(a => a.Id == areaId)
                .Include(a => a.JefeDeArea);

            return await _queryContext.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// Obtiene áreas filtradas por tipo (Facultad/Municipio) (solo lectura)
        /// </summary>
        public async Task<IEnumerable<AreaModel>> GetAreasByTipoAsync(AreaType tipo)
        {
            var query = _queryContext.Set<AreaModel>()
                .Where(a => a.Tipo == tipo)
                .Include(a => a.JefeDeArea);

            return await _queryContext.ToListAsync(query);
        }
    }
}
