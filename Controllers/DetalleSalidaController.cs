using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;
using System.Numerics;

namespace ProyectoAPI.Controllers
{
    [Route("api/detallessalida")]
    [ApiController]
    public class DetalleSalidaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DetalleSalidaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Obtener todos los detalles de una salida específica
        [HttpGet("salida/{idSalida:int}")]
        public async Task<ActionResult<List<object>>> GetBySalida(int idSalida)
        {
            var detalles = await context.DetallesSalidas
                .Include(ds => ds.Producto)
                .Where(ds => ds.IdSalida == idSalida)
                .Select(ds => new
                {
                    ds.IdSalida,
                    ds.IdProducto,
                    ProductoNombre = ds.Producto != null ? ds.Producto.Nombre : "",
                    ds.CodProducto,
                    ds.StockSalida
                })
                .ToListAsync();

            return Ok(detalles);
        }

        // Obtener un detalle específico por llave compuesta
        [HttpGet("{idSalida:int}/{idProducto:int}")]
        public async Task<ActionResult<DetalleSalida>> Get(int idSalida, int idProducto)
        {
            var detalle = await context.DetallesSalidas
                .Include(ds => ds.Producto)
                .Include(ds => ds.Salida)
                .FirstOrDefaultAsync(ds => ds.IdSalida == idSalida && ds.IdProducto == idProducto);

            if (detalle is null)
            {
                return NotFound();
            }
            return detalle;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleSalida detalleSalida)
        {
            // Verificar que la salida existe
            var salidaExiste = await context.Salidas.AnyAsync(s => s.IdSalida == detalleSalida.IdSalida);
            if (!salidaExiste)
            {
                return BadRequest("La salida no existe");
            }

            // Verificar que el producto existe
            var producto = await context.Productos.FindAsync(detalleSalida.IdProducto);
            if (producto is null)
            {
                return BadRequest("El producto no existe");
            }

            // Verificar que hay stock suficiente
            if (producto.StockActual < detalleSalida.StockSalida)
            {
                return BadRequest($"Stock insuficiente. Stock actual: {producto.StockActual}");
            }

            // Reducir el stock del producto
            producto.StockActual -= detalleSalida.StockSalida;

            context.Add(detalleSalida);
            await context.SaveChangesAsync();
            return Ok(detalleSalida);
        }

        [HttpPut("{idSalida:int}/{idProducto:int}")]
        public async Task<ActionResult> Put(int idSalida, int idProducto, [FromBody] DetalleSalida detalleSalida)
        {
            var existe = await context.DetallesSalidas
                .AnyAsync(ds => ds.IdSalida == idSalida && ds.IdProducto == idProducto);

            if (!existe)
            {
                return NotFound();
            }

            detalleSalida.IdSalida = idSalida;
            detalleSalida.IdProducto = idProducto;
            context.Update(detalleSalida);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{idSalida:int}/{idProducto:int}")]
        public async Task<ActionResult> Delete(int idSalida, int idProducto)
        {
            var detalle = await context.DetallesSalidas
                .FirstOrDefaultAsync(ds => ds.IdSalida == idSalida && ds.IdProducto == idProducto);

            if (detalle is null)
            {
                return NotFound();
            }

            // Devolver el stock al producto
            var producto = await context.Productos.FindAsync(idProducto);
            if (producto != null)
            {
                producto.StockActual += detalle.StockSalida;
            }

            context.DetallesSalidas.Remove(detalle);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}