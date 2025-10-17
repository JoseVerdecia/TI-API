using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TI_API.Domain.Enums;

namespace TI_API.Domain.Entities
{
    public class ObjetivoModel
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<ProcesoModel> Procesos { get; set; } = new();
        public EvaluacionType Evaluacion { get; set; } = EvaluacionType.NoEvaluado;
        public List<IndicadorDeObjetivoModel> IndicadoresAsignados { get; set; } = new();

        [NotMapped]
        public List<ProcesoModel> ProcesosInferidos
        {
            get
            {
                return IndicadoresAsignados
                    .Select(io => io.Indicador.Proceso)
                    .Where(p => p != null)
                    .Distinct()
                    .ToList();
            }
        }
    }
}
