using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Entities
{
    public class Producto
    {
        [Key]
        public int IdProd { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public required string Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        public required string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int StockTotal { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
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
