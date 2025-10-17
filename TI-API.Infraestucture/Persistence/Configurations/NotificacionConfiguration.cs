using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class NotificacionConfiguration : IEntityTypeConfiguration<NotificacionModel>
    {
        public void Configure(EntityTypeBuilder<NotificacionModel> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(n => n.Mensaje)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(n => n.MensajeRechazo)
                .HasMaxLength(2000);

            builder.Property(n => n.MetaCumplirPropuesta)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(n => n.Tipo)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(n => n.Estado)
                .HasConversion<string>()
                .HasDefaultValue(NotificacionState.Pendiente);

            // Relaciones
            builder.HasOne(n => n.Remitente)
                .WithMany()
                .HasForeignKey(n => n.RemitenteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Destinatario)
                .WithMany()
                .HasForeignKey(n => n.DestinatarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.Area)
                .WithMany()
                .HasForeignKey(n => n.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.IndicadorDeArea)
                .WithMany()
                .HasForeignKey(n => new { n.IndicadorDeAreaId, n.AreaId })
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(n => n.DestinatarioId);
            builder.HasIndex(n => n.Estado);
            builder.HasIndex(n => n.FechaCreacion);
        }
    }
}
