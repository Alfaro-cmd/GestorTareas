using GestorTareas_Modulo2.Domain;
using GestorTareas_Modulo2.Infrastructure;

namespace GestorTareas_Modulo2.Application;

public class GestorTareasService
{
    private IRepositorioTareas _repo;

    public GestorTareasService(IRepositorioTareas repo)
    {
        _repo = repo;
    }

    public void AgregarTarea(Tarea tarea)
    {
        _repo.Agregar(tarea);
    }

    public bool EsTituloValido(string? titulo)
    {
        return !string.IsNullOrEmpty(titulo) && titulo.Length >= 3;
    }
    public void CompletarTarea(int id)
    {
        var tarea = _repo.ObtenerPorId(id);

        if (tarea != null)
        {
            tarea.Completada = true;
        }
    }

    public Tarea BuscarPorId(int id)
    {
        return _repo.ObtenerPorId(id);
    }
    public List<Tarea> ObtenerCompletadas()
    {
        return _repo.ObtenerTodas()
            .Where(t => t.Completada)
            .Select(t => new Tarea
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Completada = t.Completada
            })
            .ToList();
    }
}