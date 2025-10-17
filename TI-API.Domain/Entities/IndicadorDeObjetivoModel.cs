using System.ComponentModel.DataAnnotations.Schema;

namespace TI_API.Domain.Entities
{
    public class IndicadorDeObjetivoModel
    {
        public int IndicadorId { get; set; }
        public int ObjetivoId { get; set; }

        [ForeignKey("IndicadorId")]
        public IndicadorModel Indicador { get; set; } = null!;

        [ForeignKey("ObjetivoId")]
        public ObjetivoModel Objetivo { get; set; } = null!;
    }
}
