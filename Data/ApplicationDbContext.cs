using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<TipoEmpleado> TiposEmpleado { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<DetalleSalida> DetallesSalidas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación 1:1 Usuario-Empleado
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empleado)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Usuario>(u => u.IdEmp)
                .OnDelete(DeleteBehavior.Restrict); // No borrar empleado si tiene usuario

            // Configurar llave compuesta para DetalleSalida
            modelBuilder.Entity<DetalleSalida>()
                .HasKey(ds => new { ds.IdSalida, ds.IdProducto });

            // Relación Salida - DetalleSalida
            modelBuilder.Entity<DetalleSalida>()
                .HasOne(ds => ds.Salida)
                .WithMany(s => s.DetallesSalidas)
                .HasForeignKey(ds => ds.IdSalida)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Producto - DetalleSalida
            modelBuilder.Entity<DetalleSalida>()
                .HasOne(ds => ds.Producto)
                .WithMany(p => p.DetallesSalidas)
                .HasForeignKey(ds => ds.IdProducto)
                .OnDelete(DeleteBehavior.Restrict); // No borrar producto si tiene salidas

            // Relación Empleado - Salida
            modelBuilder.Entity<Salida>()
                .HasOne(s => s.Empleado)
                .WithMany(e => e.Salidas)
                .HasForeignKey(s => s.IdEmp)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Categoria - Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Proveedor - Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(p => p.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación TipoEmpleado - Empleado
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.TipoEmpleado)
                .WithMany(t => t.Empleados)
                .HasForeignKey(e => e.IdTipEmp)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data inicial - Tipos de Empleado
            modelBuilder.Entity<TipoEmpleado>().HasData(
                new TipoEmpleado { IdTipEmp = 1, Nombre = "Administrador" },
                new TipoEmpleado { IdTipEmp = 2, Nombre = "Secretario" },
                new TipoEmpleado { IdTipEmp = 3, Nombre = "Almacenero" },
                new TipoEmpleado { IdTipEmp = 4, Nombre = "Vendedor" }
            );

            // Seed data inicial - Categorías
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { IdCat = 1, Descripcion = "Electrónica" },
                new Categoria { IdCat = 2, Descripcion = "Ropa" },
                new Categoria { IdCat = 3, Descripcion = "Alimentos" },
                new Categoria { IdCat = 4, Descripcion = "Juguetes" }
            );
        }
    }
}
