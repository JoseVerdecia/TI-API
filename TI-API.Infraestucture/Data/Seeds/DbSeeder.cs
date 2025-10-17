using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TI_API.Application.Common.Settings;
using TI_API.Domain.Entities;
using TI_API.Entities;

namespace TI_API.Infraestucture.Data.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRol>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var identitySettings = serviceProvider.GetRequiredService<IOptions<IdentitySettings>>().Value;

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
            var adminUser = await userManager.FindByEmailAsync(identitySettings.DefaultAdmin.Email);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = identitySettings.DefaultAdmin.Email,
                    Email = identitySettings.DefaultAdmin.Email,
                    Nombre = identitySettings.DefaultAdmin.Nombre,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, identitySettings.DefaultAdmin.Password);
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
