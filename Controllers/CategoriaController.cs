using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;

namespace ProyectoAPI.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return await context.Categorias.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerCategoriaPorId")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.IdCat == id);
            if (categoria is null)
            {
                return NotFound();
            }
            return categoria;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            var yaexisteCategoriaConNombre = await context.Categorias.AnyAsync(x => x.Descripcion == categoria.Descripcion);
            if (yaexisteCategoriaConNombre)
            {
                var mensajeDeError = $"Ya existe una categoria con el nombre {categoria.Descripcion}";
                ModelState.AddModelError(nameof(categoria.Descripcion), mensajeDeError);
                return ValidationProblem(ModelState);
            }

            context.Add(categoria);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerCategoriaPorId", new { id = categoria.IdCat }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {

            var existe = await context.Categorias.AnyAsync(x => x.IdCat == id);
            if (!existe)
            {
                return NotFound();
            }

            var yaExisteCategoriaConNombre= await context.Categorias.AnyAsync(
                x=> x.Descripcion==categoria.Descripcion && x.IdCat != id );
            var yaexisteCategoriaConNombre = await context.Categorias.AnyAsync(x => x.Descripcion == categoria.Descripcion);
            if (yaexisteCategoriaConNombre)
            {
                var mensajeDeError = $"Ya existe una categoria con el nombre {categoria.Descripcion}";
                ModelState.AddModelError(nameof(categoria.Descripcion), mensajeDeError);
                return ValidationProblem(ModelState);
            }



            categoria.IdCat = id;
            context.Update(categoria);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas = await context.Categorias.Where(x => x.IdCat == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}