using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;
using ProyectoAPI.DTOs;

namespace ProyectoAPI.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmpleadoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            var empleados = await context.Empleados
                .Include(e => e.TipoEmpleado)
                .Include(e => e.Usuario)
                .Select(e => new EmpleadoDTO
                {
                    IdEmp = e.IdEmp,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    Direccion = e.Direccion,
                    TipoEmpleado = e.TipoEmpleado != null ? e.TipoEmpleado.Nombre : "Sin tipo",
                    TieneUsuario = e.Usuario != null
                })
                .ToListAsync();

            return Ok(empleados);
        }

        [HttpGet("{id:int}", Name = "ObtenerEmpleadoPorId")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id)
        {
            var empleado = await context.Empleados
                .Include(e => e.TipoEmpleado)
                .Include(e => e.Usuario)
                .Where(e => e.IdEmp == id)
                .Select(e => new EmpleadoDTO
                {
                    IdEmp = e.IdEmp,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    Direccion = e.Direccion,
                    TipoEmpleado = e.TipoEmpleado != null ? e.TipoEmpleado.Nombre : "Sin tipo",
                    TieneUsuario = e.Usuario != null,
                    IdTipEmp = e.IdTipEmp // ← Agregar esto para editar
                })
                .FirstOrDefaultAsync();

            if (empleado is null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmpleadoCreacionDTO dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Dni = dto.Dni,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Direccion = dto.Direccion,
                IdTipEmp = dto.IdTipEmp
            };

            context.Add(empleado);
            await context.SaveChangesAsync();

            var empleadoDTO = new EmpleadoDTO
            {
                IdEmp = empleado.IdEmp,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Dni = empleado.Dni,
                Telefono = empleado.Telefono,
                Email = empleado.Email,
                Direccion = empleado.Direccion,
                TipoEmpleado = "Administrador", // Temporal
                TieneUsuario = false,
                IdTipEmp = empleado.IdTipEmp
            };

            return CreatedAtRoute("ObtenerEmpleadoPorId", new { id = empleado.IdEmp }, empleadoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmpleadoCreacionDTO dto)
        {
            var empleado = await context.Empleados.FindAsync(id);

            if (empleado is null)
            {
                return NotFound();
            }

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Dni = dto.Dni;
            empleado.Telefono = dto.Telefono;
            empleado.Email = dto.Email;
            empleado.Direccion = dto.Direccion;
            empleado.IdTipEmp = dto.IdTipEmp;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Empleados
                .Where(x => x.IdEmp == id)
                .ExecuteDeleteAsync();

            if (filasBorradas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}