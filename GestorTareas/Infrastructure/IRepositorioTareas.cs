using GestorTareas_Modulo2.Domain;
using System.Collections.Generic;
namespace GestorTareas_Modulo2.Infrastructure;

public interface IRepositorioTareas
{
    void Agregar(Tarea tarea);
    Tarea ObtenerPorId(int id);
}