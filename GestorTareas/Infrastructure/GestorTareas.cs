using GestorTareas_Modulo2.Application;
using GestorTareas_Modulo2.Domain;
using GestorTareas_Modulo2.Infrastructure;
using System.Text.Json;

namespace GestorTareas.Infrastructure;

public class GestorTareas : IRepositorioTareas
{
    private List<TareaDto> _tareas = new();

    // AGREGAR
    public void Agregar(Tarea tarea)
    {
        var tareaDto = new TareaDto
        {
            Id = tarea.Id,
            Titulo = tarea.Titulo,
            Completada = tarea.Completada
        };

        _tareas.Add(tareaDto);
    }

    // OBTENER POR ID
    public Tarea ObtenerPorId(int id)
    {
        var tareaDto = _tareas.FirstOrDefault(t => t.Id == id);

        if (tareaDto == null)
            return null;

        return new Tarea
        {
            Id = tareaDto.Id,
            Titulo = tareaDto.Titulo,
            Completada = tareaDto.Completada
        };
    }

    // GUARDAR EN JSON
    public void Guardar(string ruta)
    {
        var json = JsonSerializer.Serialize(_tareas, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(ruta, json);
    }

    // CARGAR DESDE JSON
    public void Cargar(string ruta)
    {
        if (!File.Exists(ruta))
            return;

        var json = File.ReadAllText(ruta);

        _tareas = JsonSerializer.Deserialize<List<TareaDto>>(json) ?? new List<TareaDto>();
    }

    //COMPROBAR
    public List<TareaDto> ObtenerTodas()
    {
        return _tareas;
    }
}