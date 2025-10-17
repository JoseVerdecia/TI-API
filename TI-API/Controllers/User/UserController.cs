using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TI_API.Application.Common.Constants;
using TI_API.Entities;

namespace TI_API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Todos los endpoints de este controlador requieren autenticación
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Eliminar Usuario
        [HttpDelete("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUser(string userId)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Un admin no se puede borrar a sí mismo
            if (currentUserId == userId)
            {
                return BadRequest(new { Message = "No puedes borrarte a ti mismo." });
            }

            var userToDelete = await _userManager.FindByIdAsync(userId);
            if (userToDelete == null)
            {
                return NotFound(new { Message = "Usuario no encontrado." });
            }

            // No se puede borrar al usuario con rol Admin
            var isAdmin = await _userManager.IsInRoleAsync(userToDelete, Roles.Admin);
            if (isAdmin)
            {
                return BadRequest(new { Message = "No se puede eliminar al usuario con rol de Administrador." });
            }

            var result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
            {
                return Ok(new { Message = $"Usuario '{userToDelete.Email}' eliminado correctamente." });
            }

            return StatusCode(500, new { Message = "Error al eliminar el usuario.", Errors = result.Errors });
        }



        // Endpoint de ejemplo para obtener el perfil del usuario logueado
        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                Roles = roles
            });
        }


        [HttpGet("allProfiles")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var result = users.Select(u => new
            {
                u.Id,
                u.Email,
                u.UserName,
                u.Nombre
            });
            return Ok(result);
        }

        [HttpGet("by-email/{email}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            return Ok(new { user.Id, user.Email, user.UserName, user.Nombre });
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(new { user.Id, user.Email, user.UserName, user.Nombre });
        }

        [HttpGet("by-username/{userName}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            return Ok(new { user.Id, user.Email, user.UserName, user.Nombre });
        }
    }
}