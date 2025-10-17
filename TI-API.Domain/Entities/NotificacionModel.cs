using System.ComponentModel.DataAnnotations;
using TI_API.Domain.Enums;
using TI_API.Entities;

namespace TI_API.Domain.Entities
{
    public class NotificacionModel
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public string MensajeRechazo { get; set; } = string.Empty;
        public string MetaCumplirPropuesta { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public NotificacionType Tipo { get; set; }
        public NotificacionState? Estado { get; set; }

        public Guid RemitenteId { get; set; }
        public ApplicationUser? Remitente { get; set; }

        public Guid DestinatarioId { get; set; }
        public ApplicationUser Destinatario { get; set; }

        public int AreaId { get; set; }
        public AreaModel Area { get; set; }

        public IndicadorDeAreaModel IndicadorDeArea { get; set; }
        public int IndicadorDeAreaId { get; set; }
    }
}
