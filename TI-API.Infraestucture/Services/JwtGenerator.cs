

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TI_API.Application.Common;
using TI_API.Application.Common.Interfaces;
using TI_API.Domain.Entities;
using TI_API.Entities;

namespace TI_API.Infraestucture.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRol> _roleManager;

        public JwtTokenGenerator(
            IOptions<JwtSettings> jwtSettings,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRol> roleManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthenticationResult> GenerateTokenAsync(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Nombre),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
        };

            // Agregar roles como claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthenticationResult(
                user,
                token,
                refreshToken,
                userRoles.FirstOrDefault());
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}