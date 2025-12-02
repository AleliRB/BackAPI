using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Empleado
    {
        [Key]
        public int IdEmp { get; set; }
        [StringLength(100)]
        public required String Nombre { get; set; } = string.Empty;
        [StringLength(100)]
        public required String Apellido { get; set; } = string.Empty;
        public required int Dni { get; set; }
        public required int Telefono { get; set; }
        [EmailAddress]
        [StringLength(100)]
        public required String Email { get; set; } = string.Empty;
        [StringLength(200)]
        public required String Direccion { get; set; } = string.Empty;
        // Relación con TipoEmpleado
        public int IdTipEmp { get; set; }

        [ForeignKey("IdTipEmp")]
        public TipoEmpleado? TipoEmpleado { get; set; }

        // Relación 1:1 con Usuario (opcional)
        public Usuario? Usuario { get; set; }

        // Relación con Salidas (un empleado puede generar varias salidas)
        public ICollection<Salida>? Salidas { get; set; }

    }
}
