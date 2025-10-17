using TI_API.Entities;

namespace TI_API.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<AuthenticationResult> GenerateTokenAsync(ApplicationUser user);
    }
}
