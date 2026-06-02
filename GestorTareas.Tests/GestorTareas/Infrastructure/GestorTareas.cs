using GestorTareas.Domain;
using GestorTareas.Infrastructure;

namespace GestorTareas.Tests;

public class GestorTareas : IRepositorioTareas
{
    private readonly List<Tarea> _tareas = new();

    public void Agregar(Tarea tarea)
    {
        _tareas.Add(tarea);
    }

    public Tarea? ObtenerPorId(int id)
    {
        return _tareas.FirstOrDefault(t => t.Id == id);
    }

    public List<Tarea> ObtenerTodas()
    {
        return _tareas;
    }

    public void Actualizar(Tarea tarea)
    {
        var tareaExistente = ObtenerPorId(tarea.Id);

        if (tareaExistente != null)
        {
            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Completada = tarea.Completada;
            tareaExistente.FechaLimite = tarea.FechaLimite;
            tareaExistente.UsuarioId = tarea.UsuarioId;
        }
    }

    public void Eliminar(Tarea tarea)
    {
        _tareas.Remove(tarea);
    }
}