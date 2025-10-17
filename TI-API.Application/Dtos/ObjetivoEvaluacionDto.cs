using TI_API.Domain.Enums;

namespace TI_API.Application.Dtos
{
    public class ObjetivoEvaluacionDTO
    {
        public int ObjetivoId { get; set; }
        public string ObjetivoNombre { get; set; } = string.Empty;
        public EvaluacionType Evaluacion { get; set; }
        public string EvaluacionDisplay { get; set; } = string.Empty;
        public int TotalIndicadores { get; set; }
        public int IndicadoresEsenciales { get; set; }
        public int IndicadoresNecesarios { get; set; }
        public DateTime FechaEvaluacion { get; set; }
    }
}
