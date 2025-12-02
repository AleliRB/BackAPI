using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Salida
    {
        [Key]
        public int IdSalida { get; set; }

        [StringLength(200)]
        public required string DestinoSalida { get; set; }

        public DateTime FechaSalida { get; set; }


        // Relación con Empleado
        public int IdEmp { get; set; }

        [ForeignKey("IdEmp")]
        public Empleado? Empleado { get; set; }

        // Relación con DetallesSalida
        public ICollection<DetalleSalida>? DetallesSalidas { get; set; }
    }
}
