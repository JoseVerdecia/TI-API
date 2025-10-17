using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TI_API.Infraestucture.Persistence
{
    public class CommandContextFactory : IDesignTimeDbContextFactory<CommandContext>
    {
        public CommandContext CreateDbContext(string[] args)
        {
            // Ajusta la ruta si tu appsettings.json está en otro lugar
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CommandContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new CommandContext(optionsBuilder.Options);
        }
    }
}
