using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Producto
    {
        [Key]
        public int IdProd { get; set; }

        [StringLength(100)]
        public required string Nombre { get; set; }

        [StringLength(100)]
        public required string Ubicacion { get; set; }

        [StringLength(200)]
        public required string Descripcion { get; set; }

        public int StockTotal { get; set; }

        public int StockActual { get; set; }

        // Relación con Categoria
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }

        // Relación con Proveedor
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        // Relación con DetallesSalida
        public ICollection<DetalleSalida>? DetallesSalidas { get; set; }
    }
}
