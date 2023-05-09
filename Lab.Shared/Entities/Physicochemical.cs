using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab.Shared.Entities
{
    public class Physicochemical
    {
        public int Id { get; set; }
        [Display(Name = "Residuo de Clorudo")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public string ResidualChlorine { get; set; } = null!;

        [Display(Name = "Irca")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Irca { get; set; } = null!;

        [Display(Name = "Turbidez")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Turbidity { get; set; } = null!;

        [Display(Name = "PH")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Ph { get; set; } = null!;

        [Display(Name = "Fecha Ingreso")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CollectorDate { get; set; }

        [Display(Name = "Fecha Ingreso")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string AdmissionDate { get; set; }

       
        //Relación con usuario

        //[NotMapped]
        //[JsonIgnore]
        //public User? User { get; set; }

        //[Display(Name = "Usuario")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]


        //[NotMapped]
        //[JsonIgnore]     
        //public int UserId { get; set; }


    }
}
