using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TI_API.Domain.Entities;
using TI_API.Entities;
using TI_API.Infraestucture.Persistence.Configurations;

namespace TI_API.Infraestucture.Persistence
{
    public class CommandContext : IdentityDbContext<ApplicationUser, ApplicationRol, Guid>
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options) { }

        // DbSets para escritura
        public DbSet<IndicadorModel> Indicadores { get; set; }
        public DbSet<ProcesoModel> Procesos { get; set; }
        public DbSet<ObjetivoModel> Objetivos { get; set; }
        public DbSet<AreaModel> Areas { get; set; }
        public DbSet<IndicadorDeAreaModel> IndicadoresDeArea { get; set; }
        public DbSet<IndicadorDeObjetivoModel> IndicadoresDeObjetivo { get; set; }
        public DbSet<NotificacionModel> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorConfiguration());
            modelBuilder.ApplyConfiguration(new ProcesoConfiguration());
            modelBuilder.ApplyConfiguration(new ObjetivoConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorDeAreaConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorDeObjetivoConfiguration());
            modelBuilder.ApplyConfiguration(new NotificacionConfiguration());

            // Configuración de relaciones adicionales si es necesario
            ConfigureRelationships(modelBuilder);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Configuración para la relación IndicadorDeArea
            modelBuilder.Entity<IndicadorDeAreaModel>()
                .HasOne(ia => ia.Indicador)
                .WithMany(i => i.IndicadoresAsignados)
                .HasForeignKey(ia => ia.IndicadorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IndicadorDeAreaModel>()
                .HasOne(ia => ia.Area)
                .WithMany(a => a.IndicadoresAsignados)
                .HasForeignKey(ia => ia.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración para la relación IndicadorDeObjetivo
            modelBuilder.Entity<IndicadorDeObjetivoModel>()
                .HasKey(io => new { io.IndicadorId, io.ObjetivoId });

            modelBuilder.Entity<IndicadorDeObjetivoModel>()
                .HasOne(io => io.Indicador)
                .WithMany(i => i.ObjetivosAsignados)
                .HasForeignKey(io => io.IndicadorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IndicadorDeObjetivoModel>()
                .HasOne(io => io.Objetivo)
                .WithMany(o => o.IndicadoresAsignados)
                .HasForeignKey(io => io.ObjetivoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
