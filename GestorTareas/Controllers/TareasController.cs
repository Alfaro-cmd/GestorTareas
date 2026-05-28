using GestorTareas_Modulo2.Application;
using GestorTareas_Modulo2.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas_Modulo2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly GestorTareasService _service;

    public TareasController(GestorTareasService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Tarea>> Get()
    {
        return _service.ObtenerTodas();
    }

    [HttpGet("{id}")]
    public ActionResult<Tarea> Get(int id)
    {
        var tarea = _service.ObtenerPorId(id);
        if (tarea == null) return NotFound();
        return tarea;
    }

    [HttpPost]
    public ActionResult<Tarea> Post([FromBody] Tarea tarea)
    {
        var nueva = _service.Crear(tarea.Titulo, tarea.FechaLimite, tarea.UsuarioId);
        return Ok(nueva);
    }
}