using Microsoft.EntityFrameworkCore.Storage;
using TI_API.Application.Common.Interfaces;
using TI_API.Infraestucture.Persistence;

namespace TI_API.Infraestucture.Repositories
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly CommandContext _commandContext;
        private readonly QueryContext _queryContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWorks(CommandContext commandContext, QueryContext queryContext)
        {
            _commandContext = commandContext;
            _queryContext = queryContext;

            // Inicializar repositorios con ambos contextos
            Indicador = new IndicadorRepository(_commandContext, _queryContext);
            Proceso = new ProcesoRepository(_commandContext, _queryContext);
            Objetivo = new ObjetivoRepository(_commandContext, _queryContext);
            Area = new AreaRepository(_commandContext, _queryContext);
            IndicadorDeArea = new IndicadorDeAreaRepository(_commandContext, _queryContext);
            IndicadorDeObjetivo = new IndicadorDeObjetivoRepository(_commandContext, _queryContext);
        }

        public IIndicadorRepository Indicador { get; private set; }
        public IProcesoRepository Proceso { get; private set; }
        public IObjetivoRepository Objetivo { get; private set; }
        public IAreaRepository Area { get; private set; }
        public IIndicadorDeAreaRepository IndicadorDeArea { get; private set; }
        public IIndicadorDeObjetivoRepository IndicadorDeObjetivo { get; private set; }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _commandContext.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _commandContext.SaveChangesAsync();
                await _transaction?.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction?.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _commandContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _commandContext.SaveChanges();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _commandContext.Dispose();
            _queryContext.Dispose();
        }
    }
}
