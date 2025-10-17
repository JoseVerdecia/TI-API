using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Entities
{
    public class AreaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public AreaType Tipo { get; set; }
        public List<IndicadorDeAreaModel> IndicadoresAsignados { get; set; } = new();

        public Guid? JefeDeAreaId { get; set; }

        [InverseProperty("AreaAsignada")]
        public ApplicationUser? JefeDeArea { get; set; }
    }
}
