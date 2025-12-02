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

        // Obtener todos con información de categoría y proveedor
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

        // Obtener productos por categoría
        [HttpGet("categoria/{idCategoria:int}")]
        public async Task<ActionResult<List<Producto>>> GetByCategoria(int idCategoria)
        {
            return await context.Productos.Where(p => p.IdCategoria == idCategoria).ToListAsync();
        }

        // Obtener productos con stock bajo (menos del 20% del total)
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
            // Verificar que la categoría existe
            var categoriaExiste = await context.Categorias.AnyAsync(c => c.IdCat == producto.IdCategoria);
            if (!categoriaExiste)
            {
                return BadRequest("La categoría no existe");
            }

            // Verificar que el proveedor existe
            var proveedorExiste = await context.Proveedores.AnyAsync(p => p.Id == producto.IdProveedor);
            if (!proveedorExiste)
            {
                return BadRequest("El proveedor no existe");
            }

            context.Add(producto);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProductoPorId", new { id = producto.IdProd }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Producto producto)
        {
            var existe = await context.Productos.AnyAsync(x => x.IdProd == id);
            if (!existe)
            {
                return NotFound();
            }
            producto.IdProd = id;
            context.Update(producto);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // Actualizar solo el stock
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