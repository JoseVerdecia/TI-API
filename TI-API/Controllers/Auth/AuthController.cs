using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TI_API.Entities;

namespace TI_API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Cada usuario nuevo es "UsuarioNormal"
            await _userManager.AddToRoleAsync(user, "UsuarioNormal");

            return Ok(new { Message = "Usuario registrado exitosamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }

            var token = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 días de validez
            await _userManager.UpdateAsync(user);

            return Ok(new { Token = token, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.Token);
            if (principal == null)
                return BadRequest(new { Message = "Token inválido" });

            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest(new { Message = "Token inválido" });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized(new { Message = "Refresh token inválido o expirado" });
            }

            var newToken = await GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddHours(8),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Importante: no validar expiración aquí
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }

    public record RegisterDto(string Nombre, string Email, string Password);
    public record LoginDto(string Email, string Password);
    public record RefreshTokenRequest(string Token, string RefreshToken);
}