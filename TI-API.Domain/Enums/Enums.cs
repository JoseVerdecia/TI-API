using System.ComponentModel;

namespace TI_API.Domain.Enums
{
    public enum IndicadorType
    {
        [Description("Escencial")]
        Escencial,
        [Description("Necesario")]
        Necesario
    }

    public enum IndicadorOrigen
    {
        [Description("MES")]
        MES,
        [Description("Interno")]
        Interno
    }

    public enum EvaluacionType
    {
        [Description("No Evaluado")]
        NoEvaluado,
        [Description("Incumplido")]
        Incumplido,
        [Description("Parcialmente Cumplido")]
        ParcialmenteCumplido,
        [Description("Cumplido")]
        Cumplido,
        [Description("Sobrecumplido")]
        Sobrecumplido
    }

    public enum AreaType
    {
        [Description("Facultad")]
        Facultad,
        [Description("Municipio")]
        Municipio
    }

    public enum NotificacionType
    {
        [Description("Facultad")]
        Facultad,
        [Description("Municipio")]
        Municipio
    }

    public enum NotificacionState
    {
        [Description("Recibida")]
        Recibida,
        [Description("Pendiente")]
        Pendiente,
        [Description("Denegada")]
        Denegada,
        [Description("Aceptada")]
        Aceptada
    }
}
