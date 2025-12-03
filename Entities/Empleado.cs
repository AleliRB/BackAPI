using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Empleado
    {
        [Key]
        public int IdEmp { get; set; }
        [StringLength(100)]
        [Required (ErrorMessage ="El campo {0} es requerido")]
        public required String Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public required String Apellido { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El {0} debe tener exactamente 8 dígitos")]
        public required int Dni { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El {0} debe tener exactamente 9 dígitos")]
        public required int Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        [StringLength(100)]
        public required String Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        public required String Direccion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        // Relación con TipoEmpleado
        public int IdTipEmp { get; set; }

        [ForeignKey("IdTipEmp")]
        public TipoEmpleado? TipoEmpleado { get; set; }

        // Relación 1:1 con Usuario 
        public Usuario? Usuario { get; set; }

        // Relación con Salidas 
        public ICollection<Salida>? Salidas { get; set; }

    }
}
