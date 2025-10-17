using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Enums;
using TI_API.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<AreaModel>
    {
        public void Configure(EntityTypeBuilder<AreaModel> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Tipo)
                .HasConversion<string>()
                .IsRequired();

            // Relación con JefeDeArea
            builder.HasOne(a => a.JefeDeArea)
                .WithMany()
                .HasForeignKey(a => a.JefeDeAreaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Índices
            builder.HasIndex(a => new { a.Nombre, a.Tipo })
                .IsUnique();

            builder.HasIndex(a => a.Tipo);
            builder.HasIndex(a => a.JefeDeAreaId);

            // Datos semilla de ejemplo
            builder.HasData(
                new AreaModel { Id = 1, Nombre = "FACIM", Tipo = AreaType.Facultad },
                new AreaModel { Id = 2, Nombre = "FACUP", Tipo = AreaType.Facultad },
                new AreaModel { Id = 3, Nombre = "Baguanos", Tipo = AreaType.Municipio },
                new AreaModel { Id = 4, Nombre = "Gibara", Tipo = AreaType.Municipio }
            );
        }
    }
}
