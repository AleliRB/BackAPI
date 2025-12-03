using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(255)]
        public required string ContrasenaHash { get; set; } // NUNCA "Contraseña"
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public required string TipoUsuario { get; set; } // Admin, Secretario, Almacenero
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstadoUser { get; set; } = true;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime? UltimoAcceso { get; set; }

        // Relación 1:1 con Empleado
        public int IdEmp { get; set; }

        [ForeignKey("IdEmp")]
        public Empleado? Empleado { get; set; }
    }
}
