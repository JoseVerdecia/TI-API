namespace TI_API.Application.Dtos
{
    public class IndicadorDeAreaResponseDTO
    {
        public int Id { get; set; }
        public int IndicadorId { get; set; }
        public AreaSimpleDTO Area { get; set; } = null!;
        public string MetaCumplirArea { get; set; } = string.Empty;
        public decimal DecimalMetaCumplirArea { get; set; }
        public bool IsMetaCumplirAreaPorcentage { get; set; }
        public string MetaRealArea { get; set; } = string.Empty;
        public decimal DecimalMetaRealArea { get; set; }
        public bool IsMetaRealAreaPorcentage { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public string Evaluacion { get; set; } = string.Empty;
    }
    public class AreaSimpleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}
