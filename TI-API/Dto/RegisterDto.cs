using System.ComponentModel.DataAnnotations;

namespace TI_API.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Role { get; set; }
    }
}
