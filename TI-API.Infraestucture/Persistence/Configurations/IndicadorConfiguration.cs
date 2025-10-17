using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class IndicadorConfiguration : IEntityTypeConfiguration<IndicadorModel>
    {
        public void Configure(EntityTypeBuilder<IndicadorModel> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nombre)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(i => i.Comentario)
                .HasMaxLength(1000);

            builder.Property(i => i.MetaCumplir)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.MetaReal)
                .HasMaxLength(100);

            builder.Property(i => i.DecimalMetaCumplir)
                .HasPrecision(18, 2);

            builder.Property(i => i.DecimalMetaReal)
                .HasPrecision(18, 2);

            builder.Property(i => i.Tipo)
                .HasConversion<string>();

            builder.Property(i => i.Origen)
                .HasConversion<string>();

            builder.Property(i => i.Evaluacion)
                .HasConversion<string>();

            // Relaciones
            builder.HasOne(i => i.Proceso)
                .WithMany(p => p.Indicadores)
                .HasForeignKey(i => i.ProcesoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(i => i.Nombre);
            builder.HasIndex(i => i.ProcesoId);
            builder.HasIndex(i => i.Tipo);
            builder.HasIndex(i => i.Evaluacion);
        }
    }
}
