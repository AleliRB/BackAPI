using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Obtener todos con información del empleado
        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            var usuarios = await context.Usuarios
                .Include(u => u.Empleado)
                .Select(u => new
                {
                    u.IdUser,
                    u.Nombre,
                    u.TipoUsuario,
                    u.EstadoUser,
                    u.FechaCreacion,
                    u.UltimoAcceso,
                    IdEmp = u.IdEmp,
                    EmpleadoNombre = u.Empleado != null ? u.Empleado.Nombre + " " + u.Empleado.Apellido : ""
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id:int}", Name = "ObtenerUsuarioPorId")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var usuario = await context.Usuarios
                .Include(u => u.Empleado)
                .Where(u => u.IdUser == id)
                .Select(u => new
                {
                    u.IdUser,
                    u.Nombre,
                    u.TipoUsuario,
                    u.EstadoUser,
                    u.FechaCreacion,
                    u.UltimoAcceso,
                    IdEmp = u.IdEmp,
                    EmpleadoNombre = u.Empleado != null ? u.Empleado.Nombre + " " + u.Empleado.Apellido : ""
                })
                .FirstOrDefaultAsync();

            if (usuario is null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // Obtener por IdEmp (para verificar si un empleado ya tiene usuario)
        [HttpGet("empleado/{idEmp:int}")]
        public async Task<ActionResult<Usuario>> GetByEmpleado(int idEmp)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.IdEmp == idEmp);
            if (usuario is null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            // Verificar que el empleado existe
            var empleadoExiste = await context.Empleados.AnyAsync(e => e.IdEmp == usuario.IdEmp);
            if (!empleadoExiste)
            {
                return BadRequest("El empleado no existe");
            }

            // Verificar que el empleado no tenga ya un usuario
            var usuarioExiste = await context.Usuarios.AnyAsync(u => u.IdEmp == usuario.IdEmp);
            if (usuarioExiste)
            {
                return BadRequest("Este empleado ya tiene un usuario asignado");
            }


            // Evitar error de NULL en UltimoAcceso
            usuario.UltimoAcceso = DateTime.Now;
            context.Add(usuario);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerUsuarioPorId", new { id = usuario.IdUser }, usuario);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.IdUser == id);
            if (!existe)
            {
                return NotFound();
            }
            usuario.IdUser = id;
            context.Update(usuario);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // Activar/Desactivar usuario
        [HttpPatch("{id:int}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromBody] bool estado)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario is null)
            {
                return NotFound();
            }
            usuario.EstadoUser = estado;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Usuarios.Where(x => x.IdUser == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}   