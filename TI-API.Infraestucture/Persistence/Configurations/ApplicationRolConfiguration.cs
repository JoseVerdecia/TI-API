using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRol>
    {
        public void Configure(EntityTypeBuilder<ApplicationRol> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(r => r.NormalizedName)
                .HasMaxLength(256)
                .IsRequired();

            // Índices
            builder.HasIndex(r => r.NormalizedName)
                .IsUnique()
                .HasDatabaseName("RoleNameIndex");

            // Datos semilla de roles
            builder.HasData(
                new ApplicationRol { Id = new Guid("0D3087C0-FFE7-4482-921F-588FBF057D8B"), Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRol { Id = new Guid("50EA312A-B027-4B57-9B9C-FD006E712213"), Name = "JefeProceso", NormalizedName = "JEFEPROCESO" },
                new ApplicationRol { Id = new Guid("765E0479-8871-4E35-A215-3E41670C5ED5"), Name = "JefeArea", NormalizedName = "JEFEAREA" },
                new ApplicationRol { Id = new Guid("1F0C337B-E8FE-43ED-B5E9-B49139D8F7BF"), Name = "UsuarioNormal", NormalizedName = "USUARIONORMAL" }
            );
        }
    }
}
