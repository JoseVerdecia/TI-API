using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Services
{
    public class IndicadorService
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public IndicadorService(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }

        public async Task<IEnumerable<IndicadorModel>> GetIndicadoresByEvaluacionAsync(EvaluacionType evaluacion)
        {
            return await _unitOfWorks.Indicador.GetIndicadoresByEvaluacionAsync(evaluacion);
        }

        public async Task<IEnumerable<IndicadorDeAreaModel>> GetIndicadoresDeAreaByAreaAsync(int areaId)
        {
            return await _unitOfWorks.IndicadorDeArea.GetByAreaIdAsync(areaId);
        }

        public async Task<ProcesoModel?> GetProcesoByIndicadorAsync(int indicadorId)
        {
            return await _unitOfWorks.Indicador.GetProcesoByIndicadorIdAsync(indicadorId);
        }

        public async Task CreateIndicadorWithRelationsAsync(IndicadorModel indicador, List<int> objetivosIds, List<IndicadorDeAreaModel> indicadoresDeArea)
        {
            using var transaction = await _unitOfWorks.BeginTransactionAsync();
            try
            {
                // Agregar el indicador
                await _unitOfWorks.Indicador.AddAsync(indicador);
                await _unitOfWorks.SaveChangesAsync();



                // Agregar relaciones con objetivos
                foreach (var objetivoId in objetivosIds)
                {
                    var objetivo = _unitOfWorks.Objetivo.GetByIdAsync(objetivoId);

                    if (objetivo == null)
                    {
                        return;
                    }

                    var indicadorDeObjetivo = new IndicadorDeObjetivoModel
                    {
                        IndicadorId = indicador.Id,
                        ObjetivoId = objetivoId
                    };
                    await _unitOfWorks.IndicadorDeObjetivo.AddAsync(indicadorDeObjetivo);
                }

                // Agregar indicadores de área
                foreach (var indicadorDeArea in indicadoresDeArea)
                {
                    indicadorDeArea.IndicadorId = indicador.Id;
                    await _unitOfWorks.IndicadorDeArea.AddAsync(indicadorDeArea);
                }

                await _unitOfWorks.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
