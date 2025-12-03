using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Entities
{
    public class Categoria
    {
        [Key]
        public int IdCat { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]

        public required string Descripcion { get; set; }

        // Relación inversa
        public ICollection<Producto>? Productos { get; set; }
    }
}
