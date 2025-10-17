using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TI_API.Domain.Enums;

namespace TI_API.Domain.Entities
{
    public class IndicadorModel : IEvaluableIndicador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Indicador es Obligatorio")]
        [MaxLength(250, ErrorMessage = "Nombre del Indicador muy largo")]
        public string Nombre { get; set; } = string.Empty;

        public string Comentario { get; set; } = string.Empty;
        public IndicadorType Tipo { get; set; }
        public IndicadorOrigen Origen { get; set; }

        [Required(ErrorMessage = "La meta a cumplir del Indicador es obligatoria")]
        public string MetaCumplir { get; set; } = string.Empty;
        public decimal DecimalMetaCumplir { get; set; } = 0;
        public bool IsMetaCumplirPorcentage { get; set; } = false;

        public string MetaReal { get; set; } = string.Empty;
        public decimal DecimalMetaReal { get; set; } = 0;
        public bool IsMetaRealPorcentage { get; set; } = false;

        public EvaluacionType Evaluacion { get; set; } = EvaluacionType.NoEvaluado;

        [Required(ErrorMessage = "El indicador debe tener un Proceso asignado")]
        public int ProcesoId { get; set; }

        [ForeignKey("ProcesoId")]
        public ProcesoModel Proceso { get; set; } = null!;

        public List<IndicadorDeAreaModel> IndicadoresAsignados { get; set; } = new();
        public List<IndicadorDeObjetivoModel> ObjetivosAsignados { get; set; } = new();
    }
}