using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Data
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
            public DbSet<Empleado> Empleados {  get; set; }
            public DbSet<TipoEmpleado> TiposEmpleado { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Categoria> Categorias { get; set; }
            public DbSet<Proveedor> Proveedores { get; set; }
            public DbSet<Producto> Productos { get; set; }
            public DbSet<Salida> Salidas { get; set; }
            public DbSet<DetalleSalida> DetallesSalidas { get; set; }
    }
}
