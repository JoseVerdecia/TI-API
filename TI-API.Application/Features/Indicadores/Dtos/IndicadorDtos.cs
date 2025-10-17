using TI_API.Domain.Enums;

namespace TI_API.Application.Features.Indicadores.Dtos
{
    public class IndicadorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string MetaCumplir { get; set; }
        public int ProcesoId { get; set; }
        public string Comentario { get; set; }
        public IndicadorType Tipo { get; set; }
        public IndicadorOrigen Origen { get; set; }
        public EvaluacionType Evaluacion { get; set; }
    }

    public class CreateIndicadorCommandDto
    {
        public string Nombre { get; set; }
        public string MetaCumplir { get; set; }
        public int ProcesoId { get; set; }
        public string Comentario { get; set; }
        public IndicadorType Tipo { get; set; }
        public IndicadorOrigen Origen { get; set; }
        public List<int> ObjetivosId { get; set; }
    }

    public class UpdateIndicadorCommandDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string MetaCumplir { get; set; }
        public int ProcesoId { get; set; }
        public string Comentario { get; set; }
        public IndicadorType Tipo { get; set; }
        public IndicadorOrigen Origen { get; set; }
    }
}
