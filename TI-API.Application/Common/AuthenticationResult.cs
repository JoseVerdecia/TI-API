using TI_API.Entities;

namespace TI_API.Application.Common
{
    public class AuthenticationResult
    {
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public bool Success => User != null && !string.IsNullOrEmpty(Token);
        public string[] Errors { get; set; } = Array.Empty<string>();

        public AuthenticationResult(ApplicationUser user, string token, string refreshToken, string role)
        {
            User = user;
            Token = token;
            RefreshToken = refreshToken;
            Role = role;
        }

        // Constructor para casos de error
        public AuthenticationResult(string[] errors)
        {
            Errors = errors;
        }
    }
}
