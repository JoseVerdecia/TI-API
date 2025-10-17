using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TI_API.Application.Common.Interfaces;
using TI_API.Application.Common.Settings;
using TI_API.Entities;

namespace TI_API.Application.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentitySettings _identitySettings;

        public SecurityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<IdentitySettings> identitySettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identitySettings = identitySettings.Value;
        }


        /// <summary>
        /// Verifica si el usuario con ese email y contraseña es válido.
        /// Login o autenticación manual.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null) return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        /// <summary>
        /// Obtiene el usuario por su email. 
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Verifica si el usuario pertenece a un rol específico.
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="role">Rol</param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        /// <summary>
        /// Verifica si el usuario tiene el rol "Admin"
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> IsAdminAsync(ApplicationUser user)
        {
            return await IsInRoleAsync(user, "Admin");
        }

        /// <summary>
        /// Verifica si el usuario tiene el rol "JefeProceso"
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> IsJefeProcesoAsync(ApplicationUser user)
        {
            return await IsInRoleAsync(user, "JefeProceso");
        }

        /// <summary>
        /// Verifica si el usuario tiene el rol "JefeArea"
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> IsJefeAreaAsync(ApplicationUser user)
        {
            return await IsInRoleAsync(user, "JefeArea");
        }

        /// <summary>
        /// Verifica si el usuario puede acceder a un proceso específico.
        ///   •	Admin: acceso total.
        ///   •	Jefe de proceso: solo a su proceso asignado.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="procesoId"></param>
        /// <returns></returns>
        public async Task<bool> CanAccessProcesoAsync(ApplicationUser user, int procesoId)
        {
            // Admin puede acceder a todo
            if (await IsAdminAsync(user)) return true;

            // Jefe de proceso puede acceder solo a su proceso
            if (await IsJefeProcesoAsync(user))
            {
                return user.ProcesoAsignadoId == procesoId;
            }

            return false;
        }


        /// <summary>
        /// Verifica si el usuario puede acceder a un area específica.
        ///   •	Admin: acceso total.
        ///   •	Jefe de area: solo a su area asignada.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public async Task<bool> CanAccessAreaAsync(ApplicationUser user, int areaId)
        {
            // Admin puede acceder a todo
            if (await IsAdminAsync(user)) return true;

            // Jefe de área puede acceder solo a su área
            if (await IsJefeAreaAsync(user))
            {
                return user.AreaAsignadaId == areaId;
            }

            return false;
        }

        /// <summary>
        /// Genera un token para restablecer la contraseña
        /// Se ha olvidado la contrasena ???.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        /// <summary>
        /// Restablece la contraseña usando el token generado
        /// Completa Flujo de recuperacion de contrasena.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}
