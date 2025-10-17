using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_API.Domain.Entities;
using TI_API.Domain.Enums;

namespace TI_API.Infraestucture.Persistence.Configurations
{
    public class ObjetivoConfiguration : IEntityTypeConfiguration<ObjetivoModel>
    {
        public void Configure(EntityTypeBuilder<ObjetivoModel> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Nombre)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(o => o.Evaluacion)
                .HasConversion<string>()
                .HasDefaultValue(EvaluacionType.NoEvaluado);

            // Índices
            builder.HasIndex(o => o.Nombre);
            builder.HasIndex(o => o.Evaluacion);

            // Datos semilla para los 9 objetivos estratégicos
            builder.HasData(
                new ObjetivoModel { Id = 1, Nombre = "Formar profesionales integrales, competentes, con espíritu innovador y firmeza político ideológica", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 2, Nombre = "Lograr la preparación y el completamiento del claustro y de los cuadros", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 3, Nombre = "Fortalecer el vínculo de la Educación Superior con las empresas", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 4, Nombre = "Impactar al desarrollo científico y tecnológico", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 5, Nombre = "Perfeccionar la preparación y superación de los cuadros y reservas", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 6, Nombre = "Potenciar la relación universidad-sociedad", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 7, Nombre = "Garantizar la transformación digital de las Universidades", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 8, Nombre = "Gestionar los recursos humanos, materiales y financieros", Evaluacion = EvaluacionType.NoEvaluado },
                new ObjetivoModel { Id = 9, Nombre = "Asegurar la calidad de la Educación Superior Cubana", Evaluacion = EvaluacionType.NoEvaluado }
            );
        }
    }
}
