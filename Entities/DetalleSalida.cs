using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class DetalleSalida
    {
        [Key]
        public int IdSalida { get; set; }

        [StringLength(50)]
        public required string CodProducto { get; set; }

        public int StockSalida { get; set; }

        // Relación con Salida
        [ForeignKey("IdSalida")]
        public Salida? Salida { get; set; }

        // Relación con Producto 
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }

    }
}
