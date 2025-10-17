using Microsoft.EntityFrameworkCore.Storage;

namespace TI_API.Application.Common.Interfaces
{
    public interface IUnitOfWorks : IDisposable
    {
        // Repositories
        IIndicadorRepository Indicador { get; }
        IProcesoRepository Proceso { get; }
        IObjetivoRepository Objetivo { get; }
        IAreaRepository Area { get; }
        IIndicadorDeAreaRepository IndicadorDeArea { get; }
        IIndicadorDeObjetivoRepository IndicadorDeObjetivo { get; }

        // Métodos de transacción
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        // Métodos de guardado
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
