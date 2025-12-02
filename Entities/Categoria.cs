using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Entities
{
    public class Categoria
    {
        [Key]
        public int IdCat { get; set; }

        [StringLength(100)]
        public required string Descripcion { get; set; }

        // Relación inversa
        public ICollection<Producto>? Productos { get; set; }
    }
}
