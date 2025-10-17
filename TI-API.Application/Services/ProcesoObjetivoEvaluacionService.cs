using SendGrid.Helpers.Errors.Model;
using TI_API.Application.Common.Extensions;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Dtos;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Application.Services
{
    public class ProcesoObjetivoEvaluacionService : IProcesoObjetivoEvaluacionService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IEvaluacionService<IndicadorModel> _indicadorEvaluacionService;
        private readonly IEvaluacionService<IndicadorDeAreaModel> _indicadorDeAreaEvaluacionService;

        public ProcesoObjetivoEvaluacionService(
            IUnitOfWorks unitOfWorks,
            IEvaluacionService<IndicadorModel> indicadorEvaluacionService,
            IEvaluacionService<IndicadorDeAreaModel> indicadorDeAreaEvaluacionService)
        {
            _unitOfWorks = unitOfWorks;
            _indicadorEvaluacionService = indicadorEvaluacionService;
            _indicadorDeAreaEvaluacionService = indicadorDeAreaEvaluacionService;
        }

        public async Task<EvaluacionType> EvaluarProcesoAsync(int procesoId)
        {
            var proceso = await _unitOfWorks.Proceso.GetByAsync(
                p => p.Id == procesoId,
                includeProperties: "Indicadores");

            if (proceso == null)
                throw new NotFoundException($"Proceso con ID {procesoId} no encontrado");

            return EvaluarPorIndicadores(proceso.Indicadores);
        }

        public async Task<List<ProcesoEvaluacionDTO>> EvaluarTodosProcesosAsync()
        {
            var procesos = await _unitOfWorks.Proceso.GetAllAsync(includeProperties: "Indicadores");

            var resultados = new List<ProcesoEvaluacionDTO>();

            foreach (var proceso in procesos)
            {
                var evaluacion = EvaluarPorIndicadores(proceso.Indicadores);
                resultados.Add(new ProcesoEvaluacionDTO
                {
                    ProcesoId = proceso.Id,
                    ProcesoNombre = proceso.Nombre,
                    Evaluacion = evaluacion,
                    EvaluacionDisplay = evaluacion.GetDisplayName()
                });
            }

            return resultados;
        }

        public async Task<EvaluacionType> EvaluarObjetivoAsync(int objetivoId)
        {
            var objetivo = await _unitOfWorks.Objetivo.GetByAsync(o => o.Id == objetivoId, includeProperties: "ProcesosInferidos");

            if (objetivo == null)
                throw new NotFoundException($"Objetivo con ID {objetivoId} no encontrado");

            // Obtener todos los indicadores de los procesos inferidos
            var indicadores = new List<IndicadorModel>();
            foreach (var proceso in objetivo.ProcesosInferidos)
            {
                indicadores.AddRange(proceso.Indicadores);
            }

            return EvaluarPorIndicadores(indicadores);
        }

        public async Task<List<ObjetivoEvaluacionDTO>> EvaluarTodosObjetivosAsync()
        {
            var objetivos = await _unitOfWorks.Objetivo.GetAllAsync(includeProperties: "ProcesosInferidos");

            var resultados = new List<ObjetivoEvaluacionDTO>();

            foreach (var objetivo in objetivos)
            {
                // Obtener todos los indicadores de los procesos inferidos
                var indicadores = new List<IndicadorModel>();
                foreach (var proceso in objetivo.ProcesosInferidos)
                {
                    indicadores.AddRange(proceso.Indicadores);
                }

                var evaluacion = EvaluarPorIndicadores(indicadores);
                resultados.Add(new ObjetivoEvaluacionDTO
                {
                    ObjetivoId = objetivo.Id,
                    ObjetivoNombre = objetivo.Nombre,
                    Evaluacion = evaluacion,
                    EvaluacionDisplay = evaluacion.GetDisplayName()
                });
            }

            return resultados;
        }

        private EvaluacionType EvaluarPorIndicadores(IEnumerable<IndicadorModel> indicadores)
        {
            if (!indicadores.Any())
                return EvaluacionType.NoEvaluado;

            // Actualizar evaluaciones de los indicadores
            foreach (var indicador in indicadores)
            {
                indicador.Evaluacion = _indicadorEvaluacionService.Evaluar(indicador);
            }

            // Contar indicadores por tipo y evaluación
            var indicadoresEsenciales = indicadores.Where(i => i.Tipo == IndicadorType.Escencial).ToList();
            var indicadoresNecesarios = indicadores.Where(i => i.Tipo == IndicadorType.Necesario).ToList();

            var esencialesSobrecumplidos = indicadoresEsenciales.Count(i => i.Evaluacion == EvaluacionType.Sobrecumplido);
            var esencialesCumplidos = indicadoresEsenciales.Count(i => i.Evaluacion == EvaluacionType.Cumplido);
            var esencialesParcialmenteCumplidos = indicadoresEsenciales.Count(i => i.Evaluacion == EvaluacionType.ParcialmenteCumplido);
            var esencialesIncumplidos = indicadoresEsenciales.Count(i => i.Evaluacion == EvaluacionType.Incumplido);

            var necesariosSobrecumplidos = indicadoresNecesarios.Count(i => i.Evaluacion == EvaluacionType.Sobrecumplido);
            var necesariosCumplidos = indicadoresNecesarios.Count(i => i.Evaluacion == EvaluacionType.Cumplido);
            var necesariosParcialmenteCumplidos = indicadoresNecesarios.Count(i => i.Evaluacion == EvaluacionType.ParcialmenteCumplido);
            var necesariosIncumplidos = indicadoresNecesarios.Count(i => i.Evaluacion == EvaluacionType.Incumplido);

            // Calcular porcentajes
            decimal totalEsenciales = indicadoresEsenciales.Count;
            decimal totalNecesarios = indicadoresNecesarios.Count;

            // Verificar condiciones para SOBRECUMPLIDO
            if (totalEsenciales > 0 && totalNecesarios > 0)
            {
                bool condicionSobrecumplido =
                    (esencialesSobrecumplidos / totalEsenciales) * 100 >= 60 &&
                    (esencialesCumplidos / totalEsenciales) * 100 <= 40 &&
                    esencialesParcialmenteCumplidos == 0 &&
                    esencialesIncumplidos == 0 &&
                    (necesariosSobrecumplidos / totalNecesarios) * 100 >= 50 &&
                    (necesariosCumplidos / totalNecesarios) * 100 >= 40 &&
                    (necesariosIncumplidos / totalNecesarios) * 100 <= 10;

                if (condicionSobrecumplido)
                    return EvaluacionType.Sobrecumplido;
            }

            // Verificar condiciones para CUMPLIDO
            if (totalEsenciales > 0 && totalNecesarios > 0)
            {
                bool condicionCumplido =
                    ((esencialesSobrecumplidos + esencialesCumplidos) / totalEsenciales) * 100 >= 90 &&
                    (esencialesParcialmenteCumplidos / totalEsenciales) * 100 <= 10 &&
                    esencialesIncumplidos == 0 &&
                    ((necesariosSobrecumplidos + necesariosCumplidos) / totalNecesarios) * 100 >= 70 &&
                    (necesariosParcialmenteCumplidos / totalNecesarios) * 100 >= 20 &&
                    (necesariosIncumplidos / totalNecesarios) * 100 <= 10;

                if (condicionCumplido)
                    return EvaluacionType.Cumplido;
            }

            // Verificar condiciones para PARCIALMENTE CUMPLIDO
            if (totalEsenciales > 0 && totalNecesarios > 0)
            {
                bool condicionParcialmenteCumplido =
                    ((esencialesSobrecumplidos + esencialesCumplidos + esencialesParcialmenteCumplidos) / totalEsenciales) * 100 >= 90 &&
                    (esencialesIncumplidos / totalEsenciales) * 100 <= 10 &&
                    ((necesariosSobrecumplidos + necesariosCumplidos + necesariosParcialmenteCumplidos) / totalNecesarios) * 100 >= 80 &&
                    (necesariosIncumplidos / totalNecesarios) * 100 <= 20;

                if (condicionParcialmenteCumplido)
                    return EvaluacionType.ParcialmenteCumplido;
            }

            // Si no cumple ninguna de las condiciones anteriores
            return EvaluacionType.Incumplido;
        }
    }
}