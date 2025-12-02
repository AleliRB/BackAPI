using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/proveedores")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProveedorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> Get()
        {
            return await context.Proveedores.ToListAsync();
        }

        // Obtener solo proveedores activos
        [HttpGet("activos")]
        public async Task<ActionResult<List<Proveedor>>> GetActivos()
        {
            return await context.Proveedores.Where(p => p.Estado == "Activo").ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerProveedorPorId")]
        public async Task<ActionResult<Proveedor>> Get(int id)
        {
            var proveedor = await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
            if (proveedor is null)
            {
                return NotFound();
            }
            return proveedor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Proveedor proveedor)
        {
            context.Add(proveedor);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerProveedorPorId", new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Proveedor proveedor)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            proveedor.Id = id;
            context.Update(proveedor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // Cambiar estado del proveedor
        [HttpPatch("{id:int}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromBody] string estado)
        {
            var proveedor = await context.Proveedores.FindAsync(id);
            if (proveedor is null)
            {
                return NotFound();
            }
            proveedor.Estado = estado;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Proveedores.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}