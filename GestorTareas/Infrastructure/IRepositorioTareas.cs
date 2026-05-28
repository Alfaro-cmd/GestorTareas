using GestorTareas_Modulo2.Domain;

namespace GestorTareas_Modulo2.Infrastructure;

public interface IRepositorioTareas
{
    void Agregar(Tarea tarea);

    Tarea? ObtenerPorId(int id);

    List<Tarea> ObtenerTodas();

    void Actualizar(Tarea tarea);

    void Eliminar(Tarea tarea);
}