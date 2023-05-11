using System.ComponentModel.DataAnnotations;

namespace Lab.Shared.Entities
{
    public class Microbiological
    {
        public int Id { get; set; }

        [Display(Name = "RecuentoColiformes")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
  
        public string ColiformCount { get; set; } = null!;

        [Display(Name = "Recuento Heterotrofos")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CountHeterotrophs { get; set; } = null!;

        [Display(Name = "RecuentoEColi")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string EColiCount { get; set; } = null!;
    }
}