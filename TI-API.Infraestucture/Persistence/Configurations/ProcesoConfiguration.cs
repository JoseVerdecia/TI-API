using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class ProcesoConfiguration : IEntityTypeConfiguration<ProcesoModel>
    {
        public void Configure(EntityTypeBuilder<ProcesoModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Evaluacion)
                .HasConversion<string>()
                .HasDefaultValue(EvaluacionType.NoEvaluado);

            // Relación con JefeDeProceso
            builder.HasOne(p => p.JefeDeProceso)
                .WithMany()
                .HasForeignKey(p => p.JefeDeProcesoId)
                .OnDelete(DeleteBehavior.SetNull);

            // Índices
            builder.HasIndex(p => p.Nombre)
                .IsUnique();

            builder.HasIndex(p => p.Evaluacion);
            builder.HasIndex(p => p.JefeDeProcesoId);

            // Datos semilla para los 7 procesos principales
            builder.HasData(
                new ProcesoModel { Id = 1, Nombre = "Pregrado", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 2, Nombre = "Posgrado", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 3, Nombre = "Ciencia, Tecnología e Innovación", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 4, Nombre = "Extensión Universitaria", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 5, Nombre = "Recursos Humanos", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 6, Nombre = "Información, Comunicación e Informatización", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 7, Nombre = "Internacionalización", Evaluacion = EvaluacionType.NoEvaluado },
                new ProcesoModel { Id = 8, Nombre = "Aseguramiento Material y Financiero", Evaluacion = EvaluacionType.NoEvaluado }
            );
        }
    }
}
