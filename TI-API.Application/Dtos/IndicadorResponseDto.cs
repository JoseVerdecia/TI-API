namespace TI_API.Application.Dtos
{
    public class IndicadorResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public string MetaCumplir { get; set; } = string.Empty;
        public decimal DecimalMetaCumplir { get; set; }
        public bool IsMetaCumplirPorcentage { get; set; }
        public string MetaReal { get; set; } = string.Empty;
        public decimal DecimalMetaReal { get; set; }
        public bool IsMetaRealPorcentage { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;

        // Relación con Proceso (1)
        public ProcesoSimpleDTO Proceso { get; set; } = null!;

        // Relación con Objetivos (varios)
        public List<ObjetivoSimpleDTO> ObjetivosAsignados { get; set; } = new();

        // Relación con IndicadoresDeArea (varios)
        public List<IndicadorDeAreaResponseDTO> IndicadoresDeArea { get; set; } = new();
    }

    public class ProcesoSimpleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Evaluacion { get; set; } = string.Empty;
    }

    // DTO simple para Objetivo
    public class ObjetivoSimpleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Evaluacion { get; set; } = string.Empty;
    }
}
