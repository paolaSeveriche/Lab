using Lab.Shared.Entities;
using Lab.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Lab.Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
        //compare hace las comparaciones
        [Display(Name = "Confirmación de contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
        public string PasswordConfirm { get; set; } = null!;
    }
}

