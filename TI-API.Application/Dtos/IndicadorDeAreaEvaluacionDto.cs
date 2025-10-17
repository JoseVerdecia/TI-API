using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_API.Domain.Enums;

namespace TI_API.Application.Dtos
{
    public class IndicadorDeAreaEvaluacionDTO
    {
        public int IndicadorDeAreaId { get; set; }
        public string IndicadorNombre { get; set; } = string.Empty;
        public string AreaNombre { get; set; } = string.Empty;
        public EvaluacionType Evaluacion { get; set; }
        public string EvaluacionDisplay { get; set; } = string.Empty;
        public decimal PorcentajeCumplimiento { get; set; }
        public DateTime FechaEvaluacion { get; set; }
    }
}
