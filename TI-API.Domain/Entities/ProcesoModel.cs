using TI_API.Domain.Enums;
using TI_API.Entities;

namespace TI_API.Domain.Entities
{
    public class ProcesoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public EvaluacionType Evaluacion { get; set; } = EvaluacionType.NoEvaluado;

        // Relación con Indicadores (un proceso tiene muchos indicadores)
        public ICollection<IndicadorModel> Indicadores { get; set; } = new List<IndicadorModel>();

        // Relación con el Jefe de Proceso
        public Guid? JefeDeProcesoId { get; set; }
        public ApplicationUser JefeDeProceso { get; set; }
    }
}
