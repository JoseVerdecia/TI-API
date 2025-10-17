using TI_API.Domain.Enums;

namespace TI_API.Application.Dtos
{
    public class CreateIndicadorDTO
    {
        public string Nombre { get; set; }
        public string MetaCumplir { get; set; }
        public int ProcesoId { get; set; }
        public string Comentario { get; set; }
        public IndicadorType Tipo { get; set; }
        public IndicadorOrigen Origen { get; set; }
        public List<int> ObjetivosIds { get; set; }
        public List<IndicadorDeAreaCreateDTO> IndicadoresDeArea { get; set; }
    }
}
