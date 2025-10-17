using TI_API.Domain.Enums;

namespace TI_API.Application.Dtos
{
    public class IndicadorEvaluacionDTO
    {
        public int IndicadorId { get; set; }
        public string IndicadorNombre { get; set; } = string.Empty;
        public string ProcesoNombre { get; set; } = string.Empty;
        public EvaluacionType Evaluacion { get; set; }
        public string EvaluacionDisplay { get; set; } = string.Empty;
        public decimal PorcentajeCumplimiento { get; set; }
        public DateTime FechaEvaluacion { get; set; }
    }
}
