using GestorTareas.Domain;

namespace GestorTareas.Infrastructure;

public interface IRepositorioTareas
{
    void Agregar(Tarea tarea);

    Tarea? ObtenerPorId(int id);

    List<Tarea> ObtenerTodas();

    void Actualizar(Tarea tarea);

    void Eliminar(Tarea tarea);
}