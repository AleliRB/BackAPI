using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Data;
using ProyectoAPI.Entities;
using ProyectoAPI.DTOs;
namespace ProyectoAPI.Controllers;


    [Route("api/tiposempleado")]
    [ApiController]
    public class TipoEmpleadoController: ControllerBase
    {
    private readonly ApplicationDbContext context;

    public TipoEmpleadoController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TipoEmpleado>>> Get()
    {
        return await context.TiposEmpleado.ToListAsync();
    }
    [HttpGet("{id:int}", Name = "ObtenerTipoEmpleadoPorId")]
    public async Task<ActionResult<TipoEmpleado>> Get(int id)
    {
        var tipoEmpleado = await context.TiposEmpleado.FirstOrDefaultAsync(x => x.IdTipEmp == id);
        if (tipoEmpleado is null)
        {
            return NotFound();
        }
        return tipoEmpleado;
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] TipoEmpleado tipoEmpleado)
    {
        context.Add(tipoEmpleado);
        await context.SaveChangesAsync();
        return CreatedAtRoute("ObtenerTipoEmpleado", new
        {
            id = tipoEmpleado.IdTipEmp
        }, tipoEmpleado);
        
     }
}


