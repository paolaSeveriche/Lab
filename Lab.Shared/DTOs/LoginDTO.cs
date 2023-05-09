using System.ComponentModel.DataAnnotations;

namespace Lab.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        // tiene 6 caracteres el pass
        public string Password { get; set; } = null!;
    }
}


