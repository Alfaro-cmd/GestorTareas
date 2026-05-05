using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using GestorTareas_Modulo2.Application;

namespace GestorTareas_Modulo2.Infrastructure;

public class GestorTareas
{
    private List<TareaDto> _tareas = new();

    // AGREGAR 
    public void Agregar(TareaDto tarea)
    {
        _tareas.Add(tarea);
    }

    // GUARDAR
    public void Guardar(string ruta)
    {
        var json = JsonSerializer.Serialize(
            _tareas,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(ruta, json);
    }

    // CARGAR
    public void Cargar(string ruta)
    {
        if (!File.Exists(ruta))
            return;

        var json = File.ReadAllText(ruta);

        _tareas = JsonSerializer.Deserialize<List<TareaDto>>(json) ?? new();
    }

    // SOLO PARA TEST / PROBAR
    public List<TareaDto> ObtenerTodas()
    {
        return _tareas;
    }
}