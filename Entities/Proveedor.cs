using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Entities
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="El campo {0} es requerido")]
        [StringLength(200)]
        public required string RazonSocial { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        public required string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El {0} debe tener exactamente 9 dígitos")]
        public required int Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20)]
        public required string Estado { get; set; } // Activo/Inactivo

        // Relación inversa
        public ICollection<Producto>? Productos { get; set; }
    }
}
