using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Propiedades personalizadas
            builder.Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.RefreshToken)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.RefreshTokenExpiryTime)
                .IsRequired(false);

            // Propiedades de auditoría
            builder.Property(u => u.AreaAsignadaId)
                .IsRequired(false);

            builder.Property(u => u.ProcesoAsignadoId)
                .IsRequired(false);

            // Índices para mejor rendimiento
            builder.HasIndex(u => u.Nombre);
            builder.HasIndex(u => u.AreaAsignadaId);
            builder.HasIndex(u => u.ProcesoAsignadoId);
            builder.HasIndex(u => u.RefreshToken);

            // Relaciones
            builder.HasOne(u => u.AreaAsignada)
                .WithMany()
                .HasForeignKey(u => u.AreaAsignadaId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.ProcesoAsignado)
                .WithMany()
                .HasForeignKey(u => u.ProcesoAsignadoId)
                .OnDelete(DeleteBehavior.SetNull);

            // Ignitar propiedades si es necesario
            builder.Ignore(u => u.AccessFailedCount);
            builder.Ignore(u => u.LockoutEnabled);
            builder.Ignore(u => u.LockoutEnd);
            builder.Ignore(u => u.PhoneNumber);
            builder.Ignore(u => u.PhoneNumberConfirmed);
            builder.Ignore(u => u.TwoFactorEnabled);
        }
    }
}
