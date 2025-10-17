using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TI_API.Domain.Entities;
using TI_API.Entities;

namespace TI_API.Infraestucture.Persistence.Seedings
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRol>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Crear roles 
            string[] roleNames = { "Admin", "JefeProceso", "JefeArea", "UsuarioNormal" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRol(roleName));
                }
            }

            // Crear usuario administrador
            var adminEmail = "admin@universidad.edu";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombre = "Administrador General",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin123!"); // Cambia esta contraseña
            }

            // Asignar rol de Admin si no lo tiene
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}