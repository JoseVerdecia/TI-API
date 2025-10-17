using TI_API.Application.Dtos;
using TI_API.Domain.Enums;

namespace TI_API.Application.Common.Interfaces
{
    public interface IProcesoObjetivoEvaluacionService
    {
        // Evaluar un proceso específico
        Task<EvaluacionType> EvaluarProcesoAsync(int procesoId);

        // Evaluar todos los procesos
        Task<List<ProcesoEvaluacionDTO>> EvaluarTodosProcesosAsync();

        // Evaluar un objetivo específico
        Task<EvaluacionType> EvaluarObjetivoAsync(int objetivoId);

        // Evaluar todos los objetivos
        Task<List<ObjetivoEvaluacionDTO>> EvaluarTodosObjetivosAsync();




    }
}
