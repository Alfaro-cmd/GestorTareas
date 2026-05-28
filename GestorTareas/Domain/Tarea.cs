namespace GestorTareas.Domain;

public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public bool Completada { get; set; }
    public DateTime? FechaLimite { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}