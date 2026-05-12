namespace GestorTareas_Modulo2.Application;

public class TareaDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime FechaLimite { get; set; }
    public int Prioridad { get; set; }
    public string Estado { get; set; }
    public bool Completada { get; internal set; }
}