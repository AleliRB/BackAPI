using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/empleados")]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmpleadoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //PROGRAMACIÓN ASINCRONA
        [HttpGet]
        public async Task<List<Empleado>> Get()
        {
            return await context.Empleados.ToListAsync();
        }
        //EDITAR UNA LAPTOP
        [HttpGet("{id:int}", Name = "ObtenerEmpleadoPorId")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await context.Empleados.FirstOrDefaultAsync(x => x.IdEmp == id);
            if (empleado is null)
            {
                return NotFound();
            }
            return empleado;
        }

        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody] Empleado empleado)
        {
            context.Add(empleado);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerEmpleadoPorId", new { id = empleado.IdEmp }, empleado);

        }

        //ACTUALIZAR
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Empleado empleado)
        {
            var existeEmpleado = await context.Empleados.AnyAsync(X => X.IdEmp == id);
            if (!existeEmpleado)
            {
                return NotFound();
            }
            empleado.IdEmp = id;
            context.Update(empleado);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Empleados.Where(x => x.IdEmp == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}

