using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Entities
{
    public class TipoEmpleado
    {

        [Key]
        public int IdTipEmp { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public required string Nombre { get; set; } // Admin, Secretario, Almacenero

        // Relación inversa
        public ICollection<Empleado>? Empleados { get; set; }
    }
}
