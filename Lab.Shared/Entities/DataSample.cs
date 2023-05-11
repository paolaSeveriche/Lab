using System.ComponentModel.DataAnnotations;

namespace Lab.Shared.Entities
{
    public class DataSample
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de Recolección")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CollectorDate { get; set; } = null!;

        [Display(Name = "Fecha de Ingreso")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string AdmissionDate { get; set; } = null!;


        [Display(Name = "Hora")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Hours { get; set; } = null!;



        [Display(Name = "Nombre de Recolector")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NameR { get; set; } = null!;


        [Display(Name = "Temperatura de ingreso")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string temperature { get; set; } = null!;

        [Display(Name = "Tipo Frasco")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string RecentType { get; set; } = null!;

        [Display(Name = "Cantidad")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Amount { get; set; } = null!;

        [Display(Name = "Color")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Color { get; set; } = null!;

        [Display(Name = "Olor")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Smell { get; set; } = null!;

        [Display(Name = "Aspecto")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Aspect { get; set; } = null!;

    }
}
