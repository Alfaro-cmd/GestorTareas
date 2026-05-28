using GestorTareas.Domain;
using GestorTareas.Infrastructure;

namespace GestorTareas.Application;

public class GestorTareasService
{
    private readonly IRepositorioTareas _repositorio;

    public GestorTareasService(IRepositorioTareas repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Tarea> ObtenerTodas()
    {
        return _repositorio.ObtenerTodas();
    }

    public Tarea? ObtenerPorId(int id)
    {
        return _repositorio.ObtenerPorId(id);
    }

    public Tarea Crear(string titulo, DateTime? fechaLimite, int usuarioId)
    {
        var tarea = new Tarea
        {
            Titulo = titulo,
            FechaLimite = fechaLimite,
            UsuarioId = usuarioId,
            Completada = false
        };

        _repositorio.Agregar(tarea);

        return tarea;
    }

    public void Crear(Tarea tarea)
    {
        throw new NotImplementedException();
    }
}