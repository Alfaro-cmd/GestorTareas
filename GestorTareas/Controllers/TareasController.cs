using GestorTareas.Application;
using GestorTareas.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly GestorTareasService _service;

        public TareasController(GestorTareasService service)
        {
            _service = service;
        }

        // GET TODAS
        [HttpGet]
        public ActionResult<List<Tarea>> Get()
        {
            return _service.ObtenerTodas();
        }

        // GET POR ID
        [HttpGet("{id}")]
        public ActionResult<Tarea> Get(int id)
        {
            var tarea = _service.ObtenerPorId(id);

            if (tarea == null)
                return NotFound();

            return tarea;
        }

        // POST (CREAR)
        [HttpPost]
        public ActionResult<Tarea> Post([FromBody] Tarea tarea)
        {
            var nueva = _service.Crear(tarea.Titulo, tarea.FechaLimite, tarea.UsuarioId);

            return Ok(nueva);
        }

        // PUT (ACTUALIZAR)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Tarea tarea)
        {
            var existente = _service.ObtenerPorId(id);

            if (existente == null)
                return NotFound();

            existente.Titulo = tarea.Titulo;
            existente.FechaLimite = tarea.FechaLimite;
            existente.UsuarioId = tarea.UsuarioId;

            _service.Actualizar(existente);

            return Ok("Tarea actualizada");
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarea = _service.ObtenerPorId(id);

            if (tarea == null)
                return NotFound();

            _service.Eliminar(tarea);

            return Ok("Tarea eliminada");
        }
    }
}