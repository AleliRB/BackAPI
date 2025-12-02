using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/salidas")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SalidaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Obtener todas con información del empleado
        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            var salidas = await context.Salidas
                .Include(s => s.Empleado)
                .Select(s => new
                {
                    s.IdSalida,
                    s.DestinoSalida,
                    s.FechaSalida,
                    
                    IdEmp = s.IdEmp,
                    EmpleadoNombre = s.Empleado != null ? s.Empleado.Nombre + " " + s.Empleado.Apellido : ""
                })
                .ToListAsync();

            return Ok(salidas);
        }

        [HttpGet("{id:int}", Name = "ObtenerSalidaPorId")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var salida = await context.Salidas
                .Include(s => s.Empleado)
                .Include(s => s.DetallesSalidas)
                    .ThenInclude(ds => ds.Producto)
                .Where(s => s.IdSalida == id)
                .Select(s => new
                {
                    s.IdSalida,
                    s.DestinoSalida,
                    s.FechaSalida,
               
                    IdEmp = s.IdEmp,
                    EmpleadoNombre = s.Empleado != null ? s.Empleado.Nombre + " " + s.Empleado.Apellido : "",
                    Detalles = s.DetallesSalidas!.Select(ds => new
                    {
                        ds.IdProducto,
                        ProductoNombre = ds.Producto != null ? ds.Producto.Nombre : "",
                        ds.CodProducto,
                        ds.StockSalida
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (salida is null)
            {
                return NotFound();
            }
            return Ok(salida);
        }

        // Obtener salidas por empleado
        [HttpGet("empleado/{idEmp:int}")]
        public async Task<ActionResult<List<Salida>>> GetByEmpleado(int idEmp)
        {
            return await context.Salidas
                .Where(s => s.IdEmp == idEmp)
                .Include(s => s.DetallesSalidas)
                .ToListAsync();
        }

        // Obtener salidas pendientes de devolución
        [HttpGet("pendientes")]
        public async Task<ActionResult<List<object>>> GetPendientes()
        {
            var salidas = await context.Salidas
                .Include(s => s.Empleado)
                .Select(s => new
                {
                    s.IdSalida,
                    s.DestinoSalida,
                    s.FechaSalida,
                    EmpleadoNombre = s.Empleado != null ? s.Empleado.Nombre + " " + s.Empleado.Apellido : ""
                })
                .ToListAsync();

            return Ok(salidas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Salida salida)
        {
            // Verificar que el empleado existe
            var empleadoExiste = await context.Empleados.AnyAsync(e => e.IdEmp == salida.IdEmp);
            if (!empleadoExiste)
            {
                return BadRequest("El empleado no existe");
            }

            context.Add(salida);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerSalidaPorId", new { id = salida.IdSalida }, salida);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Salida salida)
        {
            var existe = await context.Salidas.AnyAsync(x => x.IdSalida == id);
            if (!existe)
            {
                return NotFound();
            }
            salida.IdSalida = id;
            context.Update(salida);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // Registrar devolución
        [HttpPatch("{id:int}/devolucion")]
        public async Task<ActionResult> RegistrarDevolucion(int id, [FromBody] DateTime fechaDevolucion)
        {
            var salida = await context.Salidas
                .Include(s => s.DetallesSalidas)
                .FirstOrDefaultAsync(s => s.IdSalida == id);

            if (salida is null)
            {
                return NotFound();
            }

          

            // Actualizar stock de productos
            if (salida.DetallesSalidas != null)
            {
                foreach (var detalle in salida.DetallesSalidas)
                {
                    var producto = await context.Productos.FindAsync(detalle.IdProducto);
                    if (producto != null)
                    {
                        producto.StockActual += detalle.StockSalida;
                    }
                }
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Salidas.Where(x => x.IdSalida == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}