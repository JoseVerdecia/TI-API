using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class IndicadorDeObjetivoConfiguration : IEntityTypeConfiguration<IndicadorDeObjetivoModel>
    {
        public void Configure(EntityTypeBuilder<IndicadorDeObjetivoModel> builder)
        {
            // Clave compuesta
            builder.HasKey(io => new { io.IndicadorId, io.ObjetivoId });

            // Relaciones
            builder.HasOne(io => io.Indicador)
                .WithMany(i => i.ObjetivosAsignados)
                .HasForeignKey(io => io.IndicadorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(io => io.Objetivo)
                .WithMany(o => o.IndicadoresAsignados)
                .HasForeignKey(io => io.ObjetivoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(io => io.ObjetivoId);
        }
    }
}
