using TI_API.Domain.Enums;

namespace TI_API.Domain.Entities
{
    public interface IEvaluableIndicador
    {
        decimal DecimalMetaCumplir { get; set; }
        decimal DecimalMetaReal { get; set; }
        bool IsMetaCumplirPorcentage { get; set; }
        bool IsMetaRealPorcentage { get; set; }
        string MetaCumplir { get; set; }
        string MetaReal { get; set; }
        string Comentario { get; set; }
        EvaluacionType Evaluacion { get; set; }
    }
}
