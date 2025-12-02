using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Entities
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public required string RazonSocial { get; set; }

        [StringLength(200)]
        public required string Direccion { get; set; }

        public required int Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [StringLength(20)]
        public required string Estado { get; set; } // Activo/Inactivo

        // Relación inversa
        public ICollection<Producto>? Productos { get; set; }
    }
}
