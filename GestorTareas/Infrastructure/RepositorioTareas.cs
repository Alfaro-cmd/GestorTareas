using GestorTareas.Domain;
using GestorTareas.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Infrastructure;

public class RepositorioTareas : IRepositorioTareas
{
    private readonly AppDbContext _context;

    public RepositorioTareas(AppDbContext context)
    {
        _context = context;
    }

    public void Agregar(Tarea tarea)
    {
        _context.Tareas.Add(tarea);
        _context.SaveChanges();
    }

    public Tarea? ObtenerPorId(int id)   
    {
        return _context.Tareas
            .Include(t => t.Usuario)
            .FirstOrDefault(t => t.Id == id);
    }

    public List<Tarea> ObtenerTodas()
    {
        return _context.Tareas.ToList();
    }

    public void Actualizar(Tarea tarea)
    {
        _context.Tareas.Update(tarea);
        _context.SaveChanges();
    }

    public void Eliminar(Tarea tarea)
    {
        _context.Tareas.Remove(tarea);
        _context.SaveChanges();
    }
}