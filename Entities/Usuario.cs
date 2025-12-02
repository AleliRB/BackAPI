using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUser { get; set; }

        [StringLength(100)]
        public required string Nombre { get; set; }

        [StringLength(255)]
        public required string ContraseñaHash { get; set; } // NUNCA "Contraseña"

        [StringLength(50)]
        public required string TipoUsuario { get; set; } // Admin, Secretario, Almacenero

        public bool EstadoUser { get; set; } = true;

        // Relación 1:1 con Empleado
        public int IdEmp { get; set; }

        [ForeignKey("IdEmp")]
        public Empleado? Empleado { get; set; }
    }
}
