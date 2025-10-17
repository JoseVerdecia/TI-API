using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TI_API.Domain.Enums;
using TI_API.Entities;

namespace TI_API.Domain.Entities
{
    public class IndicadorDeAreaModel : IEvaluableIndicador
    {
        [Key]
        public int Id { get; set; }
        public int IndicadorId { get; set; }
        public int AreaId { get; set; }

        [Required]
        public string MetaCumplir { get; set; }
        public decimal DecimalMetaCumplir { get; set; }
        public bool IsMetaCumplirPorcentage { get; set; }
        public string MetaReal { get; set; }
        public decimal DecimalMetaReal { get; set; }
        public bool IsMetaRealPorcentage { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public EvaluacionType Evaluacion { get; set; } = EvaluacionType.NoEvaluado;

        [ForeignKey("IndicadorId")]
        public IndicadorModel Indicador { get; set; } = null!;

        [ForeignKey("AreaId")]
        public AreaModel Area { get; set; } = null!;
    }
}
