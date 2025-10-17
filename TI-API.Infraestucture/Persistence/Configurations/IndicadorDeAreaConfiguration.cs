using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class IndicadorDeAreaConfiguration : IEntityTypeConfiguration<IndicadorDeAreaModel>
    {
        public void Configure(EntityTypeBuilder<IndicadorDeAreaModel> builder)
        {
            // Clave compuesta
            builder.HasKey(ia => new { ia.IndicadorId, ia.AreaId });

            builder.Property(ia => ia.MetaCumplirArea)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ia => ia.MetaRealArea)
                .HasMaxLength(100);

            builder.Property(ia => ia.DecimalMetaCumplirArea)
                .HasPrecision(18, 2);

            builder.Property(ia => ia.DecimalMetaRealArea)
                .HasPrecision(18, 2);

            builder.Property(ia => ia.Comentario)
                .HasMaxLength(1000);

            builder.Property(ia => ia.Evaluacion)
                .HasConversion<string>();

            // Relaciones
            builder.HasOne(ia => ia.Indicador)
                .WithMany(i => i.IndicadoresAsignados)
                .HasForeignKey(ia => ia.IndicadorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ia => ia.Area)
                .WithMany(a => a.IndicadoresAsignados)
                .HasForeignKey(ia => ia.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(ia => ia.AreaId);
            builder.HasIndex(ia => ia.Evaluacion);
        }
    }
}
