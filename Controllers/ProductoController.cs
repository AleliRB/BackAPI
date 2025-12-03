using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            var productos = await context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Select(p => new
                {
                    p.IdProd,
                    p.Nombre,
                    p.Ubicacion,
                    p.Descripcion,
                    p.StockTotal,
                    p.StockActual,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria != null ? p.Categoria.Descripcion : "",
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor != null ? p.Proveedor.RazonSocial : ""
                })
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id:int}", Name = "ObtenerProductoPorId")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var producto = await context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Where(p => p.IdProd == id)
                .Select(p => new
                {
                    p.IdProd,
                    p.Nombre,
                    p.Ubicacion,
                    p.Descripcion,
                    p.StockTotal,
                    p.StockActual,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria != null ? p.Categoria.Descripcion : "",
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor != null ? p.Proveedor.RazonSocial : ""
                })
                .FirstOrDefaultAsync();

            if (producto is null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        
        [HttpGet("categoria/{idCategoria:int}")]
        public async Task<ActionResult<List<Producto>>> GetByCategoria(int idCategoria)
        {
            return await context.Productos.Where(p => p.IdCategoria == idCategoria).ToListAsync();
        }

        
        [HttpGet("stockbajo")]
        public async Task<ActionResult<List<object>>> GetStockBajo()
        {
            var productos = await context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.StockActual < (p.StockTotal * 0.2))
                .Select(p => new
                {
                    p.IdProd,
                    p.Nombre,
                    p.StockTotal,
                    p.StockActual,
                    Categoria = p.Categoria != null ? p.Categoria.Descripcion : ""
                })
                .ToListAsync();

            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            
            var existeNombre = await context.Productos.AnyAsync(p => p.Nombre == producto.Nombre);
            if (existeNombre)
            {
                ModelState.AddModelError(nameof(Producto.Nombre),
                    $"Ya existe un producto con el nombre {producto.Nombre}");
                return ValidationProblem(ModelState);
            }

           
            var categoriaExiste = await context.Categorias.AnyAsync(c => c.IdCat == producto.IdCategoria);
            if (!categoriaExiste)
            {
                ModelState.AddModelError(nameof(Producto.IdCategoria), "La categoría no existe");
                return ValidationProblem(ModelState);
            }

            var proveedorExiste = await context.Proveedores.AnyAsync(p => p.Id == producto.IdProveedor);
            if (!proveedorExiste)
            {
                ModelState.AddModelError(nameof(Producto.IdProveedor), "El proveedor no existe");
                return ValidationProblem(ModelState);
            }

            context.Add(producto);
            await context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerProductoPorId",
                new { id = producto.IdProd },
                producto);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

          
            var productoBD = await context.Productos.FindAsync(id);
            if (productoBD == null)
                return NotFound();

            var existeNombre = await context.Productos
                .AnyAsync(p => p.Nombre == producto.Nombre && p.IdProd != id);

            if (existeNombre)
            {
                ModelState.AddModelError(nameof(Producto.Nombre),
                    $"Ya existe otro producto con el nombre {producto.Nombre}");
                return ValidationProblem(ModelState);
            }

            var categoriaExiste = await context.Categorias.AnyAsync(c => c.IdCat == producto.IdCategoria);
            if (!categoriaExiste)
            {
                ModelState.AddModelError(nameof(Producto.IdCategoria),
                    "La categoría no existe");
                return ValidationProblem(ModelState);
            }

            var proveedorExiste = await context.Proveedores.AnyAsync(p => p.Id == producto.IdProveedor);
            if (!proveedorExiste)
            {
                ModelState.AddModelError(nameof(Producto.IdProveedor),
                    "El proveedor no existe");
                return ValidationProblem(ModelState);
            }

            
            productoBD.Nombre = producto.Nombre;
            productoBD.Descripcion = producto.Descripcion;
            productoBD.IdCategoria = producto.IdCategoria;
            productoBD.IdProveedor = producto.IdProveedor;

            await context.SaveChangesAsync();

            return NoContent();
        }


       
        [HttpPatch("{id:int}/stock")]
        public async Task<ActionResult> ActualizarStock(int id, [FromBody] int nuevoStock)
        {
            var producto = await context.Productos.FindAsync(id);
            if (producto is null)
            {
                return NotFound();
            }
            producto.StockActual = nuevoStock;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Productos.Where(x => x.IdProd == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}