using TI_API.Entities;

namespace TI_API.Application.Common.Interfaces
{
    public interface ISecurityService
    {
        Task<bool> ValidateUserAsync(string email, string password);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<bool> IsAdminAsync(ApplicationUser user);
        Task<bool> IsJefeProcesoAsync(ApplicationUser user);
        Task<bool> IsJefeAreaAsync(ApplicationUser user);
        Task<bool> CanAccessProcesoAsync(ApplicationUser user, int procesoId);
        Task<bool> CanAccessAreaAsync(ApplicationUser user, int areaId);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
    }
}
