using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TI_API.Domain.Entities;

namespace TI_API.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        // Relación con Area
        public int? AreaAsignadaId { get; set; }

        [ForeignKey("AreaAsignadaId")]
        public AreaModel? AreaAsignada { get; set; }


        // Relación con Proceso
        public int? ProcesoAsignadoId { get; set; }

        [ForeignKey("ProcesoAsignadoId")]
        public ProcesoModel? ProcesoAsignado { get; set; }


        // Agregar estas propiedades para el refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
